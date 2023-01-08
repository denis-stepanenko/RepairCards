using Dapper;
using RepairCardsDapperData.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RepairCardsDapperData.Data
{
    public class ProductRelationRepo : RepoBase
    {
        public IEnumerable<ProductRelation> GetProductEntries(int productId, int tableId, int type)
        {
            string tm = DateTime.Now.ToString("HHmmssffff");

            using (var transaction = conn.BeginTransaction())
            {
                try
                {
                    conn.Execute("c_SelTask", new { Tm = tm }, commandType: CommandType.StoredProcedure, transaction: transaction);

                    conn.Execute("i_TaskComp", new
                    {
                        IdDse = productId,
                        CountDse = 1,
                        IdOrder = 0,
                        Reference = 1,
                        TableWhat = tableId,
                        Tm = tm
                    }, commandType: CommandType.StoredProcedure, transaction: transaction);

                    conn.Execute("i_CompoundTaskDevelopment", new
                    {
                        IdentOpen = 1,
                        Tm = tm
                    }, commandType: CommandType.StoredProcedure, transaction: transaction);

                    var result = conn.Query("EVPR_sCompoundForDepts",
                        new { Tm = tm, IdentPurchase = type }, commandType: CommandType.StoredProcedure, transaction: transaction)
                        .Select(x => new ProductRelation
                        {
                            Code = x.DecNumWhat,
                            Name = x.Name,
                            ParentCode =  string.IsNullOrWhiteSpace(x.DecNumIn) ? null : x.DecNumIn,
                            Count = (int)Math.Ceiling(x.Count),
                            CountAll = (int)Math.Ceiling(x.CountAll),
                            CountAllWithoutWaste = (int)Math.Ceiling(x.CountAllWithoutTO),
                            Route = x.Dept,
                            Type = x.TypeWhat,
                            TypeName = x.TypeName
                        });

                    transaction.Commit();
                    return result;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public IEnumerable<ProductRelation> GetProductRelations(string code, int count)
        {
            string tm = DateTime.Now.ToString("HHmmssffff");

            using (var tran = conn.BeginTransaction())
            {
                try
                {
                    int id = conn.ExecuteScalar<int>("select Id from ref_dse where Decnum = @Code", new { Code = code }, transaction: tran);

                    conn.Execute("c_SelTask", new { Tm = tm }, commandType: CommandType.StoredProcedure, transaction: tran);

                    conn.Execute("i_TaskComp", new
                    {
                        IdDse = id,
                        CountDse = count,
                        IdOrder = 0,
                        Reference = 1,
                        TableWhat = 2,
                        Tm = tm
                    }, commandType: CommandType.StoredProcedure, transaction: tran);

                    conn.Execute("i_CompoundTaskDevelopmentWithTypeIn", new
                    {
                        IdentOpen = 1,
                        Tm = tm
                    }, commandType: CommandType.StoredProcedure, transaction: tran);

                    var result = conn.Query("EVPR_sCompoundForDeptsWithTypeIn",
                        new { Tm = tm, IdentPurchase = 0 }, commandType: CommandType.StoredProcedure, transaction: tran)
                        .Select(x => new ProductRelation
                        {
                            Code = x.DecNumWhat,
                            Name = x.Name,
                            ParentCode = string.IsNullOrWhiteSpace(x.DecNumIn) ? null : x.DecNumIn,
                            Count = Convert.ToDecimal(x.Count),
                            TechWaste = Convert.ToDecimal(x.TechWaste),
                            CountAll = Convert.ToDecimal(x.CountAll),
                            CountAllWithoutWaste = Convert.ToDecimal(x.CountAllWithoutTO),
                            Route = x.Dept,
                            Type = x.TypeWhat,
                            ParentType = string.IsNullOrWhiteSpace(x.DecNumIn) ? null : x.TypeIn,
                            TypeName = x.TypeName,
                            AssemblyDepartment = x.Dept21?.ToString(),
                            IsAssembly = Convert.ToBoolean(x.Sb)
                        });

                    tran.Commit();
                    return result;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

    }
}

using Dapper;
using RepairCardsDapperData.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RepairCardsDapperData.Data
{
    public class ProductOperationRepo : RepoBase
    {
        public string GetRouteByProduct(int productId, int tableId)
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
                        new
                        {
                            Tm = tm,
                            IdentPurchase = 0
                        }, commandType: CommandType.StoredProcedure, transaction: transaction);

                    string route = result.Where(x => string.IsNullOrWhiteSpace(x.DecNumIn)).Select(x => x.Dept).FirstOrDefault();


                    transaction.Commit();
                    return route.Replace("  ", " ");
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public string GetRoute(string productCode)
        {
            var product = conn.Query(
@"select r.[what], r.[tablewhat] from 
(
    select [decnum], [name], 2 [tablewhat], [id] [what] from ref_dse
    union
    select [decnum], [prodname] + ' ' + [texpar] + ' ' + [gost] [name], 1 [tablewhat], [id] [what] from ref_purchase
    union
    select [decnum], [name], 5 [tablewhat], [id] [what] from ref_kalk
) r where r.[decnum] = @Code", new { Code = productCode }).Single();

            return GetRouteByProduct(product.what, product.tablewhat);
        }

        public IEnumerable<ProductOperation> GetProductOperations(string productCode)
        {
                var product = conn.Query(
@"select r.[what], r.[tablewhat] from 
(
    select [decnum], [name], 2 [tablewhat], [id] [what] from ref_dse
    union
    select [decnum], [prodname] + ' ' + [texpar] + ' ' + [gost] [name], 1 [tablewhat], [id] [what] from ref_purchase
    union
    select [decnum], [name], 5 [tablewhat], [id] [what] from ref_kalk
) r where r.[decnum] = @Code", new { Code = productCode }).Single();

            string route = GetRouteByProduct(product.what, product.tablewhat);

                var operations = conn.Query("s_TechProc____1", new
                {
                    Decnum = productCode,
                    Dept = "000",
                    CodeZak = "",
                    Nol = "0",
                    Marshryt = route,
                    Key = 1
                }, commandType: CommandType.StoredProcedure)
                    .Select(x => new ProductOperation
                    {
                        Code = x.CodeOp,
                        Name = x.Name,
                        Labor = (decimal)x.Labor,
                        Department = Convert.ToInt32(x.Dept)
                    });

                return operations;
        }

        public IEnumerable<ProductOperation> GetProductOperations(string productCode, string route)
        {
            var operations = conn.Query("s_TechProc____1", new
            {
                Decnum = productCode,
                Dept = "000",
                CodeZak = "",
                Nol = "0",
                Marshryt = route,
                Key = 1
            }, commandType: CommandType.StoredProcedure)
                .Select(x => new ProductOperation
                {
                    Code = x.CodeOp,
                    Name = x.Name,
                    Labor = (decimal)x.Labor,
                    Department = Convert.ToInt32(x.Dept)
                });

            return operations;
        }

        public IEnumerable<ProductOperation> GetProductOperationsByDepartment(string productCode, int department)
        {
            var operations = GetProductOperations(productCode);
            return operations.Where(x => x.Department == department);
        }

        public IEnumerable<ProductOperation> GetProductOperationsByDepartment(string productCode, string route, int department)
        {
            var operations = GetProductOperations(productCode, route);
            return operations.Where(x => x.Department == department);
        }

    }
}

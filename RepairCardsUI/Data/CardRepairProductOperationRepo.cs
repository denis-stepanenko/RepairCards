using Dapper;
using RepairCardsDapperData.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepairCardsDapperData.Data
{
    public class CardRepairProductOperationRepo : RepoBase
    {
        public CardRepairProductOperation Get(int id) => conn.Query<CardRepairProductOperation, Executor, CardRepairProductOperation>(
@"select o.*, e.* from CRCardRepairProductOperations o 
left join CRExecutors e on e.Id = o.ExecutorId
where o.Id = @Id",
(o, e) => { o.Executor = e; return o; },
new { Id = id }).Single();

        public IEnumerable<CardRepairProductOperation> GetAll(int productId) => conn.Query<CardRepairProductOperation, Executor, CardRepairProductOperation>(
@"select o.*, e.* from CRCardRepairProductOperations o 
left join CRExecutors e on e.Id = o.ExecutorId
where o.CardRepairProductId = @ProductId",
(o, e) => { o.Executor = e; return o; },
new { ProductId = productId });
         
        public int Add(CardRepairProductOperation item) => conn.ExecuteScalar<int>(
@"insert into CRCardRepairProductOperations
(CardRepairProductId, Code, Name, Labor, Count, LaborAll, Date, Department, UnitName, GroupName, ExecutorId)
values
(@CardRepairProductId, @Code, @Name, @Labor, @Count, @LaborAll, @Date, @Department, @UnitName, @GroupName, @ExecutorId);
select scope_identity();", item);

        public void Update(CardRepairProductOperation item) => conn.Execute(
@"update CRCardRepairProductOperations
set
CardRepairProductId = @CardRepairProductId,
Code = @Code,
Name = @Name,
[Count] = @Count,
Labor = @Labor,
[Date] = @Date,
LaborAll = @LaborAll,
UnitName = @UnitName,
GroupName = @GroupName,
Department = @Department,
ExecutorId = @ExecutorId
where Id = @Id", item);

        public void Delete(int id) => conn.Execute(
"delete from CRCardRepairProductOperations where Id = @Id", 
new { Id = id });

    }
}

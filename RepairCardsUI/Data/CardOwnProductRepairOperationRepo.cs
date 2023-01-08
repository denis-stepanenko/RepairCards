using Dapper;
using RepairCardsDapperData.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepairCardsDapperData.Data
{
    public class CardOwnProductRepairOperationRepo : RepoBase
    {
        public CardOwnProductRepairOperation Get(int id) => conn.Query<CardOwnProductRepairOperation, Executor, CardOwnProductRepairOperation>(
@"select o.*, e.* from CRCardOwnProductRepairOperations o 
left join CRExecutors e on e.Id = o.ExecutorId
where o.Id = @Id",
(o, e) => { o.Executor = e; return o; },
new { Id = id }).Single();

        public IEnumerable<CardOwnProductRepairOperation> GetAll(int productId) => conn.Query<CardOwnProductRepairOperation, Executor, CardOwnProductRepairOperation>(
@"select o.*, e.* from CRCardOwnProductRepairOperations o 
left join CRExecutors e on e.Id = o.ExecutorId
where o.CardOwnProductId = @CardOwnProductId",
(o, e) => { o.Executor = e; return o; },
new { CardOwnProductId = productId });

        public int Add(CardOwnProductRepairOperation item) => conn.ExecuteScalar<int>(
@"insert into CRCardOwnProductRepairOperations
(CardOwnProductId, Code, Name, Labor, Count, LaborAll, Date, Department, UnitName, GroupName, ExecutorId)
values
(@CardOwnProductId, @Code, @Name, @Labor, @Count, @LaborAll, @Date, @Department, @UnitName, @GroupName, @ExecutorId);
select scope_identity();", item);

        public void Update(CardOwnProductRepairOperation item) => conn.Execute(
@"update CRCardOwnProductRepairOperations
set
CardOwnProductId = @CardOwnProductId,
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
@"delete from CRCardOwnProductRepairOperations where Id = @Id",
new { Id = id });

        public void Confirm(CardOwnProductRepairOperation item, int userId, string username) => conn.Execute(
@"update CRCardOwnProductRepairOperations 
set 
ConfirmUserId = @ConfirmUserId,
ConfirmUserName = @ConfirmUserName
where Id = @Id",
new { Id = item.Id, ConfirmUserId = userId, ConfirmUserName = username });

        public void Unconfirm(CardOwnProductRepairOperation item) => conn.Execute(
@"update CRCardOwnProductRepairOperations 
set 
ConfirmUserId = null,
ConfirmUserName = null
where Id = @Id",
item);
       
    }
}

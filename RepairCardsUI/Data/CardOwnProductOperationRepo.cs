using Dapper;
using RepairCardsDapperData.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepairCardsDapperData.Data
{
    public class CardOwnProductOperationRepo : RepoBase
    {
        public IEnumerable<CardOwnProductOperation> GetAllByProduct(int ownProductId) => conn.Query<CardOwnProductOperation, Executor, CardOwnProductOperation>(
@"select o.*, e.* from CRCardOwnProductOperations o 
left join CRExecutors e on e.Id = o.ExecutorId
where o.CardOwnProductId = @CardOwnProductId",
(o, e) => { o.Executor = e; return o; },
new { CardOwnProductId = ownProductId });

        public CardOwnProductOperation Get(int id) => conn.Query<CardOwnProductOperation, Executor, CardOwnProductOperation>(
@"select o.*, e.* from CRCardOwnProductOperations o 
left join CRExecutors e on e.Id = o.ExecutorId
where o.Id = @Id",
(o, e) => { o.Executor = e; return o; },
new { Id = id }).Single();

        public int Add(CardOwnProductOperation cardOwnProductOperation) => conn.ExecuteScalar<int>(
@"insert into CRCardOwnProductOperations
(CardOwnProductId, Code, Name, Labor, Type, Date, Department, ExecutorId)
values
(@CardOwnProductId, @Code, @Name, @Labor, @Type, @Date, @Department, @ExecutorId);
select scope_identity();", cardOwnProductOperation);

        public void Update(CardOwnProductOperation cardOperation) => conn.Execute(
@"update CRCardOwnProductOperations
set
CardOwnProductId = @CardOwnProductId,
Code = @Code,
Name = @Name,
Labor = @Labor,
Type = @Type,
Date = @Date,
Department = @Department,
ExecutorId = @ExecutorId
where Id = @Id", cardOperation);

        public void Delete(int cardOwnProductOperationId) => conn.Execute(
 "delete from CRCardOwnProductOperations where Id = @Id",
 new { Id = cardOwnProductOperationId });

        public void Confirm(CardOwnProductOperation item, int userId, string username) => conn.Execute(
@"update CRCardOwnProductOperations 
set 
ConfirmUserId = @ConfirmUserId,
ConfirmUserName = @ConfirmUserName
where Id = @Id",
new { Id = item.Id, ConfirmUserId = userId, ConfirmUserName = username });

        public void Unconfirm(CardOwnProductOperation item) => conn.Execute(
@"update CRCardOwnProductOperations 
set 
ConfirmUserId = null,
ConfirmUserName = null
where Id = @Id",
item);
        
    }
}

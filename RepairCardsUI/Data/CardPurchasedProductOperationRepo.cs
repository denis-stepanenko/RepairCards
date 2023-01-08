using Dapper;
using RepairCardsDapperData.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepairCardsDapperData.Data
{
    public class CardPurchasedProductOperationRepo : RepoBase
    {
        public IEnumerable<CardPurchasedProductOperation> GetAllByProduct(int purchasedProductId) => conn.Query<CardPurchasedProductOperation, Executor, CardPurchasedProductOperation>(
@"select o.*, e.* from CRCardPurchasedProductOperations o 
left join CRExecutors e on e.Id = o.ExecutorId
where o.CardPurchasedProductId = @CardPurchasedProductId",
(o, e) => { o.Executor = e; return o; },
new { CardPurchasedProductId = purchasedProductId });

        public CardPurchasedProductOperation Get(int id) => conn.Query<CardPurchasedProductOperation, Executor, CardPurchasedProductOperation>(
@"select o.*, e.* from CRCardPurchasedProductOperations o 
left join CRExecutors e on e.Id = o.ExecutorId
where o.Id = @Id",
(o, e) => { o.Executor = e; return o; },
new { Id = id }).Single();

        public int Add(CardPurchasedProductOperation item) => conn.ExecuteScalar<int>(
@"insert into CRCardPurchasedProductOperations
(CardPurchasedProductId, Code, Name, Labor, Type, Date, Department, ExecutorId)
values
(@CardPurchasedProductId, @Code, @Name, @Labor, @Type, @Date, @Department, @ExecutorId);
select scope_identity();", item);

        public void Delete(int id) => conn.Execute(
"delete from CRCardPurchasedProductOperations where Id = @Id",
new { Id = id });

        public void Update(CardPurchasedProductOperation item) => conn.Execute(
@"update CRCardPurchasedProductOperations
set
CardPurchasedProductId = @CardPurchasedProductId,
Code = @Code,
Name = @Name,
Labor = @Labor,
Type = @Type,
Date = @Date,
Department = @Department,
ExecutorId = @ExecutorId
where Id = @Id", item);

        public void Confirm(CardPurchasedProductOperation item, int userId, string username) => conn.Execute(
@"update CRCardPurchasedProductOperations 
set 
ConfirmUserId = @ConfirmUserId,
ConfirmUserName = @ConfirmUserName
where Id = @Id",
new { Id = item.Id, ConfirmUserId = userId, ConfirmUserName = username });

        public void Unconfirm(CardPurchasedProductOperation item) => conn.Execute(
@"update CRCardPurchasedProductOperations 
set 
ConfirmUserId = null,
ConfirmUserName = null
where Id = @Id",
item);

    }
}

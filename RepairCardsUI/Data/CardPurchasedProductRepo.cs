using Dapper;
using RepairCardsDapperData.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepairCardsDapperData.Data
{
    public class CardPurchasedProductRepo : RepoBase
    {
        public CardPurchasedProduct Get(int id) => conn.Query<CardPurchasedProduct>(
"select * from CRCardPurchasedProducts where Id = @Id",
new { Id = id }).Single();

        public IEnumerable<CardPurchasedProduct> GetAllByCard(int cardId) => conn.Query<CardPurchasedProduct>(
"select * from CRCardPurchasedProducts where CardId = @CardId",
new { CardId = cardId });

        public IEnumerable<CardPurchasedProduct> GetAllByCards(List<int> ids) => conn.Query<CardPurchasedProduct, Card, CardPurchasedProduct>(
@"select * from CRCardPurchasedProducts p 
join CRCards c on c.Id = p.CardId
where p.CardId in @Ids
order by c.Number",
(p, c) => { p.Card = c; return p; },
new { Ids = ids });

        public int Add(CardPurchasedProduct item) => conn.ExecuteScalar<int>(
@"insert into CRCardPurchasedProducts
(CardId, Code, Name, Count)
values
(@CardId, @Code, @Name, @Count);
select scope_identity();", item);

        public void Update(CardPurchasedProduct item) => conn.Execute(
@"update CRCardPurchasedProducts 
set
CardId = @CardId,
Code = @Code,
Name = @Name,
Count = @Count
where Id = @Id", item);

        public void Delete(int id) => conn.Execute(
"delete from CRCardPurchasedProducts where Id = @Id",
new { Id = id });

    }
}

using Dapper;
using RepairCardsDapperData.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepairCardsDapperData.Data
{
    public class CardRepairProductRepo : RepoBase
    {
        public CardRepairProduct Get(int id) => conn.Query<CardRepairProduct>(
"select * from CRCardRepairProducts where Id = @Id",
new { Id = id }).Single();

        public IEnumerable<CardRepairProduct> GetAll(int cardId) => conn.Query<CardRepairProduct>(
"select * from CRCardRepairProducts where CardId = @CardId",
new { CardId = cardId });

        public int Add(CardRepairProduct item) => conn.ExecuteScalar<int>(
@"insert into CRCardRepairProducts
(CardId, Code, Name, Count)
values
(@CardId, @Code, @Name, @Count);
select scope_identity();", item);

        public void Update(CardRepairProduct item) => conn.Execute(
@"update CRCardRepairProducts 
set
CardId = @CardId,
Code = @Code,
Name = @Name,
Count = @Count
where Id = @Id", item);

        public void Delete(int id) => conn.Execute(
"delete from CRCardRepairProducts where Id = @Id",
new { Id = id });

    }
}

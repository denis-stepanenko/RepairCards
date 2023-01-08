using Dapper;
using RepairCardsDapperData.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepairCardsDapperData.Data
{
    public class CardDocumentRepo : RepoBase
    {
        public CardDocument Get(int id) => conn.Query<CardDocument>(
"select * from CRCardDocuments where Id = @Id",
new { Id = id }).Single();

        public IEnumerable<CardDocument> GetAllByCard(int cardId) => conn.Query<CardDocument>(
"select * from CRCardDocuments where CardId = @CardId",
new { CardId = cardId });

        public int Add(CardDocument item) => conn.ExecuteScalar<int>(
@"insert into CRCardDocuments
(CardId, Name) values (@CardId, @Name);
select scope_identity();", item);

        public void Update(CardDocument item) => conn.Execute(
@"update CRCardDocuments 
set
CardId = @CardId,
Name = @Name
where Id = @Id", item);

        public void Delete(int id) => conn.Execute(
"delete from CRCardDocuments where Id = @Id",
new { Id = id });

    }
}

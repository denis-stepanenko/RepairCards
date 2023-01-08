using Dapper;
using RepairCardsDapperData.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepairCardsDapperData.Data
{
    public class CardMaterialRepo : RepoBase
    {
        public CardMaterial Get(int id) => conn.Query<CardMaterial, MaterialUnit, CardMaterial>(
@"select m.Id, m.Code, m.Name, m.Size, m.Type, m.UnitId, m.Count, m.Price, m.CardId, m.RowVersion, m.Department, u.UnitId Id, u.Code, u.Name, u.FullName from CRCardMaterials m 
left join tUnit u on m.UnitId = u.UnitId
where m.Id = @Id",
(m, u) => { m.Unit = u; return m; },
new { Id = id }).Single();

        public IEnumerable<CardMaterial> GetAllByCard(int cardId) => conn.Query<CardMaterial, MaterialUnit, CardMaterial>(
@"select m.Id, m.Code, m.Name, m.Size, m.Type, m.UnitId, m.Count, m.Price, m.CardId, m.RowVersion, m.Department, u.UnitId Id, u.Code, u.Name, u.FullName from CRCardMaterials m 
left join tUnit u on m.UnitId = u.UnitId
where m.CardId = @CardId",
(m, u) => { m.Unit = u; return m; },
new { CardId = cardId });

        public IEnumerable<CardMaterial> GetAllByCards(List<int> ids) => conn.Query<CardMaterial, MaterialUnit, Card, CardMaterial>(
@"select m.Id, m.Code, m.Name, m.Size, m.Type, m.UnitId, m.Count, m.Price, m.CardId, m.RowVersion, m.Department, u.UnitId Id, u.Code, u.Name, u.FullName, c.* from CRCardMaterials m 
left join tUnit u on m.UnitId = u.UnitId
left join CRCards c on c.Id = m.CardId
where m.CardId in @Ids
order by c.Number",
(m, u, c) => { m.Unit = u; m.Card = c; return m; },
new { Ids = ids });

        public int Add(CardMaterial item) => conn.ExecuteScalar<int>(
@"insert into CRCardMaterials
(CardId, Code, Name, Size, Type, Count, Price, UnitId, Department)
values
(@CardId, @Code, @Name, @Size, @Type, @Count, @Price, @UnitId, @Department);
select scope_identity();", item);

        public void Update(CardMaterial item) => conn.Execute(
@"update CRCardMaterials 
set
CardId = @CardId,
Code = @Code,
Name = @Name,
Size = @Size,
Type = @Type,
Count = @Count,
Price = @Price,
UnitId = @UnitId,
Department = @Department
where Id = @Id", item);

        public void Delete(int id) => conn.Execute(
"delete from CRCardMaterials where Id = @Id",
new { Id = id });

    }
}

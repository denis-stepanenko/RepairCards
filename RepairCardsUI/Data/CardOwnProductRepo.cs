using Dapper;
using RepairCardsDapperData.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace RepairCardsDapperData.Data
{
    public class CardOwnProductRepo : RepoBase
    {
        public CardOwnProduct Get(int id) => conn.Query<CardOwnProduct>(
"select * from CRCardOwnProducts where Id = @Id",
new { Id = id }).Single();

        public IEnumerable<CardOwnProduct> GetAllByCard(int cardId) => conn.Query<CardOwnProduct>(
"select * from CRCardOwnProducts where CardId = @CardId",
new { CardId = cardId });

        public IEnumerable<CardOwnProduct> GetAllByCardWithCountAll(int cardId) => conn.Query<CardOwnProduct>(
@"with cte
as
(
	select *, Count CountAll from CRCardOwnProducts where CardId = @Id and ParentId is null
	union all
	select c.*, c.Count * cte.CountAll CountAll from cte
	join CRCardOwnProducts c on c.ParentId = cte.Id
)
select * from cte",
new { Id = cardId });

        public IEnumerable<CardOwnProduct> GetAllByCards(List<int> ids) => conn.Query<CardOwnProduct, Card, CardOwnProduct>(
@"select * from CRCardOwnProducts p 
join CRCards c on c.Id = p.CardId
where p.CardId in @Ids
order by c.Number",
(p, c) => { p.Card = c; return p; },
new { Ids = ids });

        public bool IsProductAddedAfterAddingFunctionalityForHierarchicalStorageOfProducts(int id) => conn.ExecuteScalar<bool>(
"select case when (select HasChangedComposition from CRCardOwnProducts where Id = @Id) is not null then 1 else 0 end",
new { Id = id});

        public int Add(CardOwnProduct item) => conn.ExecuteScalar<int>(
@"insert into CRCardOwnProducts
(CardId, Code, Name, Count, Route, ParentId, HasChangedComposition, IsOvercoatingRequired)
values
(@CardId, @Code, @Name, @Count, @Route, @ParentId, @HasChangedComposition, @IsOvercoatingRequired);
select scope_identity();", item);

        public void Update(CardOwnProduct item)
        {
            using (var tran = conn.BeginTransaction())
            {
                try
                {
                    conn.Execute(
@"update CRCardOwnProducts 
set
CardId = @CardId,
Code = @Code,
Name = @Name,
Count = @Count,
Route = @Route,
ParentId = @ParentId,
HasChangedComposition = @HasChangedComposition,
IsOvercoatingRequired = @IsOvercoatingRequired
where Id = @Id", item, tran);

                    // Помечаем узел и всех его родителей, как измененные
                    conn.Execute(
@"with cte
as
(
	select * from CRCardOwnProducts where Id = @Id
	union all
	select c.* from cte
	join CRCardOwnProducts c on c.Id = cte.ParentId
)
update CRCardOwnProducts 
set HasChangedComposition = 1
where Id in
(
	select Id from cte
)", item, tran);

                    tran.Commit();
                }
                catch (SqlException)
                {
                    tran.Rollback();
                    throw;
                }
                
            }
        }

        public void Delete(int id) => conn.Execute(
"delete from CRCardOwnProducts where Id = @Id",
new { Id = id });

        public void DeleteRecursively(int id) => conn.Execute(
@"with cte
as
(
	select * from CRCardOwnProducts where Id = @Id
	union all
	select c.* from cte
	join CRCardOwnProducts c on c.ParentId = cte.Id
)
delete from CRCardOwnProducts where Id in (select Id from cte)", new { Id = id });

        public bool AreThereChildProductsWithRepairOperationsWithDepartment(int id, int department) => conn.ExecuteScalar<bool>(
@"with cte
as
(
	select * from CRCardOwnProducts where Id = @Id
	union all
	select c.* from cte
	join CRCardOwnProducts c on c.ParentId = cte.Id
)

select case when exists (
    select * from CRCardOwnProducts p
    join cte c on c.Id = p.Id
    join CRCardOwnProductRepairOperations o on o.CardOwnProductId = p.Id
    where o.Department = @Department
) then 1 else 0 end", new { Id = id, Department = department });

        public bool AreThereProductsWithChangedComposition(Card card) => conn.ExecuteScalar<bool>(
@"select case when exists(select * from CRCardOwnProducts where CardId = @Id and ParentId is null and HasChangedComposition = 1) then 1 else 0 end", card);

        public void MarkIfOvercoatingRequired(int id) => conn.Execute(
@"update CRCardOwnProducts
set IsOvercoatingRequired = ~IsOvercoatingRequired
where Id = @Id", 
new { Id = id });

    }
}

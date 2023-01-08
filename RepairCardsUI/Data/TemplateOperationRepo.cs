using Dapper;
using RepairCardsDapperData.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepairCardsDapperData.Data
{
    public class TemplateOperationRepo : RepoBase
    {
        public TemplateOperation Get(int templateOperationId) => conn.Query<TemplateOperation, Operation, TemplateOperation>(
@"select * from CRTemplateOperations t
left join CROperations o on t.OperationId = o.Id
where t.Id = @Id",
(t, o) => { t.Operation = o; return t; },
new { Id = templateOperationId }).Single();

        public IEnumerable<TemplateOperation> GetAll(int templateId) => conn.Query<TemplateOperation, Operation, TemplateOperation>(
@"select * from CRTemplateOperations t
left join CROperations o on t.OperationId = o.Id
where TemplateId = @TemplateId
order by Number",
(t, o) => { t.Operation = o; return t; },
new { TemplateId = templateId });

        public int Add(TemplateOperation item) => conn.ExecuteScalar<int>(
@"insert into CRTemplateOperations
(TemplateId, OperationId, Count, Number) values (@TemplateId, @OperationId, @Count, (select isnull(max(Number), 0) + 1 from CRTemplateOperations where TemplateId = @TemplateId))
select scope_identity();", item);

        public void Update(TemplateOperation item) => conn.Execute(
@"update CRTemplateOperations
set
TemplateId = @TemplateId,
OperationId = @OperationId,
Count = @Count
where Id = @Id", item);

        public void Delete(int id) => conn.Execute(
"delete from CRTemplateOperations where Id = @Id",
new { Id = id });

        public void Swap(TemplateOperation item1, TemplateOperation item2) => conn.Execute(
@"if exists (select * from CRTemplateOperations where Id = @Id1 and Number = @Number1)
and exists (select * from CRTemplateOperations where Id = @Id2 and Number = @Number2)
update CRTemplateOperations set Number = @Number2 where Id = @Id1
update CRTemplateOperations set Number = @Number1 where Id = @Id2",
new
{
    Id1 = item1.Id,
    Id2 = item2.Id,
    Number1 = item1.Number,
    Number2 = item2.Number
});

    }
}

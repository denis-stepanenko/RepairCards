using Dapper;
using RepairCardsDapperData.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepairCardsDapperData.Data
{
    public class CardOperationRepo : RepoBase
    {
        public CardOperation Get(int id) => conn.Query<CardOperation, Executor, CardOperation>(
@"select * from CRCardOperations o
left join CRExecutors e on e.Id = o.ExecutorId
where o.Id = @Id",
(o, e) => { o.Executor = e; return o; },
new { Id = id }).FirstOrDefault();

        public IEnumerable<CardOperation> GetAllByCardAndType(int cardId, int type) => conn.Query<CardOperation, Executor, CardOperation>(
@"select * from CRCardOperations o
left join CRExecutors e on e.Id = o.ExecutorId
where o.CardId = @CardId and o.[Type] = @Type
order by o.Id",
(o, e) => { o.Executor = e; return o; },
new { CardId = cardId, Type = type });

        public IEnumerable<CardOperation> GetAllByCard(int cardId) => conn.Query<CardOperation, Executor, CardOperation>(
@"select * from CRCardOperations o
left join CRExecutors e on e.Id = o.ExecutorId
where o.CardId = @CardId",
(o, e) => { o.Executor = e; return o; },
new { CardId = cardId });

        public IEnumerable<CardOperation> GetAllByCards(List<int> ids) => conn.Query<CardOperation, Executor, Card, CardOperation>(
@"select * from CRCardOperations o
left join CRExecutors e on e.Id = o.ExecutorId
left join CRCards c on c.Id = o.CardId
where o.CardId in @Ids
order by c.Number",
(o, e, c) => { o.Executor = e; o.Card = c; return o; },
new { Ids = ids });

        public IEnumerable<CardOperation> GetAllByDepartment(int cardId, int type, int department) => conn.Query<CardOperation, Executor, CardOperation>(
@"select * from CRCardOperations o
left join CRExecutors e on e.Id = o.ExecutorId
where o.CardId = @CardId and o.[Type] = @Type and o.Department = @Department",
(o, e) => { o.Executor = e; return o; },
new { CardId = cardId, Type = type, Department = department });


        public int Add(CardOperation cardOperation) => conn.ExecuteScalar<int>(
@"insert into CRCardOperations
(CardId, Code, Name, [Count], Labor, [Type], [Date], LaborAll, UnitName, GroupName, Department, ExecutorId, TemplateCode, TemplateName, Comment)
values
(@CardId, @Code, @Name, @Count, @Labor, @Type, @Date, @LaborAll, @UnitName, @GroupName, @Department, @ExecutorId, @TemplateCode, @TemplateName, @Comment);
select scope_identity();", cardOperation);

        public void Update(CardOperation cardOperation) => conn.Execute(
@"update CRCardOperations
set
Code = @Code,
Name = @Name,
[Count] = @Count,
Labor = @Labor,
[Type] = @Type,
[Date] = @Date,
LaborAll = @LaborAll,
UnitName = @UnitName,
GroupName = @GroupName,
Department = @Department,
ExecutorId = @ExecutorId,
TemplateCode = @TemplateCode,
TemplateName = @TemplateName,
Comment = @Comment
where Id = @Id", cardOperation);

        public void Delete(int cardOperationId) => conn.Execute(
"delete from CRCardOperations where Id = @Id",
new { Id = cardOperationId });

        public void Confirm(CardOperation item, int userId, string username) => conn.Execute(
@"update CRCardOperations 
set 
ConfirmUserId = @ConfirmUserId,
ConfirmUserName = @ConfirmUserName
where Id = @Id",
new { Id = item.Id, ConfirmUserId = userId, ConfirmUserName = username });

        public void Unconfirm(CardOperation item) => conn.Execute(
@"update CRCardOperations 
set 
ConfirmUserId = null,
ConfirmUserName = null
where Id = @Id",
item);

        public bool IsConfirmed(CardOperation item) => conn.ExecuteScalar<bool>(
@"select count(*) from CRCardOperations where Id = @Id and ConfirmUserId is not null", item);

    }
}

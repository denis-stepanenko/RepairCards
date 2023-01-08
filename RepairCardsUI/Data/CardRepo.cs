using Dapper;
using RepairCardsDapperData.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepairCardsDapperData.Data
{
    public class CardRepo : RepoBase
    {
        public int Add(Card card, User user)
        {
            using (var tran = conn.BeginTransaction())
            {
                try
                {
                    int id = conn.ExecuteScalar<int>(
$@"insert into CRCards
(Number, ParentId, ParentId2, RepairTypeId, Department, [Order], Stage, FactoryNumber, Direction, Cipher, ClientOrder,
ProductId, ProductCode, ProductName, InvoiceNumber, Date, Source, ReasonForRepair, CreatorName, CardStatusId, ActNumber, PermissionCardNumber,
IsDepartment4Confirmed, IsDepartment5Confirmed, IsDepartment6Confirmed, IsDepartment13Confirmed, IsDepartment17Confirmed, 
IsDepartment80Confirmed, IsDepartment82Confirmed)
values
(@Number, @ParentId, @ParentId2, @RepairTypeId, @Department, @Order, @Stage, @FactoryNumber, @Direction, @Cipher, @ClientOrder,
@ProductId, @ProductCode, @ProductName, @InvoiceNumber, @Date, @Source, @ReasonForRepair, '{user.Name}', @CardStatusId, @ActNumber, @PermissionCardNumber,
@IsDepartment4Confirmed, @IsDepartment5Confirmed, @IsDepartment6Confirmed, @IsDepartment13Confirmed, @IsDepartment17Confirmed, 
@IsDepartment80Confirmed, @IsDepartment82Confirmed);
select scope_identity();", card, transaction: tran);

                    conn.Execute(
@"insert into CRCardDetails
(Id, ExternalDefects, InternalDefects, Malfunctions)
values
(@Id, null, null, null)", new { Id = id }, transaction: tran);

                    tran.Commit();
                    return id;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        public Card Get(int id) => conn.Query<Card, RepairType, CardStatus, Card>(
@"select c.*, t.*, s.* from CRCards c
left join CRRepairTypes t on c.RepairTypeId = t.Id
left join CRCardStatuses s on c.CardStatusId = s.Id
where c.Id = @Id",
(c, t, s) => { c.RepairType = t; c.CardStatus = s; return c; },
new { Id = id }).FirstOrDefault();

        public IEnumerable<Card> GetAll() => conn.Query<Card, RepairType, CardStatus, Card>(
@"select c.*, 
(select top 1 Number from CRCards p where p.Id = c.ParentId) ParentNumber,
(case when (select count(*) from CRCardConfirmations a where a.CardId = c.Id and a.UserRoleId = 6 and a.CardConfirmationObjectId = 1) > 0 then 1 else 0 end) IsConfirmed,
t.*,
s.*
from CRCards c
left join CRRepairTypes t on c.RepairTypeId = t.Id
left join CRCardStatuses s on c.CardStatusId = s.Id
order by c.Id desc",
(c, t, s) => { c.RepairType = t; c.CardStatus = s; return c; });

        public IEnumerable<Card> GetAll(string filter) => conn.Query<Card, RepairType, CardStatus, Card>(
@"select top 100 c.*, 
(select top 1 Number from CRCards p where p.Id = c.ParentId) ParentNumber,
(case when (select count(*) from CRCardConfirmations a where a.CardId = c.Id and a.UserRoleId = 6 and a.CardConfirmationObjectId = 1) > 0 then 1 else 0 end) IsConfirmed,
t.*,
s.*
from CRCards c
left join CRRepairTypes t on c.RepairTypeId = t.Id
left join CRCardStatuses s on c.CardStatusId = s.Id
where Number like '%' + @Filter + '%' or ProductCode like '%' + @Filter + '%' or ProductName like '%' + @Filter + '%'
order by c.Id desc",
(c, t, s) => { c.RepairType = t; c.CardStatus = s; return c; },
new { Filter = filter });

        public IEnumerable<Card> GetAllByParent(int id) => conn.Query<Card, RepairType, CardStatus, Card>(
@"select c.*, 
(select top 1 Number from CRCards p where p.Id = c.ParentId) ParentNumber,
t.*,
s.*
from CRCards c
left join CRRepairTypes t on c.RepairTypeId = t.Id
left join CRCardStatuses s on c.CardStatusId = s.Id
where c.ParentId = @Id
order by c.Id desc",
(c, t, s) => { c.RepairType = t; c.CardStatus = s; return c; },
new { Id = id });

        public IEnumerable<Card> GetAllInstalledCardsByParent(int id) => conn.Query<Card, RepairType, CardStatus, Card>(
@"select c.*, 
t.*,
s.*
from CRCards c
left join CRRepairTypes t on c.RepairTypeId = t.Id
left join CRCardStatuses s on c.CardStatusId = s.Id
where c.ParentId2 = @Id
order by c.Id desc",
(c, t, s) => { c.RepairType = t; c.CardStatus = s; return c; },
new { Id = id });


        public IEnumerable<Card> GetAllByParentRecursive(int id) => conn.Query<Card>(
@"with cte
as
(
	select * from CRCards where Id = @Id
	union all
	select c.* from cte
	join CRCards c on c.ParentId = cte.Id
)
select * from cte",
new { Id = id });

        public IEnumerable<Card> GetAllInstalledProductCardsByParentRecursive(int id) => conn.Query<Card>(
@"with cte
as
(
	select * from CRCards where Id = @Id
	union all
	select c.* from cte
	join CRCards c on c.ParentId2 = cte.Id
)
select * from cte",
new { Id = id });

        public string GetNewNumber(int department) => conn.ExecuteScalar<string>(
$"select cast(isnull(max(convert(int, left(Number, charindex('/', Number) - 1))), 0) + 1 as varchar(max)) + '/{department}.' + right(year(getdate()), 2) from CRCards where Number like '%/{department}.' + right(year(getdate()), 2)");

        public void Update(Card item) => conn.Execute(
@"update CRCards 
set
Number = @Number,
ParentId = @ParentId,
ParentId2 = @ParentId2,
RepairTypeId = @RepairTypeId,
Department = @Department,
[Order] = @Order,
Stage = @Stage,
FactoryNumber = @FactoryNumber,
Direction = @Direction,
Cipher = @Cipher,
ClientOrder = @ClientOrder,
ProductId = @ProductId,
ProductCode = @ProductCode,
ProductName = @ProductName,
InvoiceNumber = @InvoiceNumber,
Date = @Date,
Source = @Source,
ReasonForRepair = @ReasonForRepair,
CardStatusId = @CardStatusId,
ActNumber = @ActNumber,
PermissionCardNumber = @PermissionCardNumber,
IsDepartment4Confirmed = @IsDepartment4Confirmed,
IsDepartment5Confirmed = @IsDepartment5Confirmed,
IsDepartment6Confirmed = @IsDepartment6Confirmed,
IsDepartment13Confirmed = @IsDepartment13Confirmed,
IsDepartment17Confirmed = @IsDepartment17Confirmed,
IsDepartment80Confirmed = @IsDepartment80Confirmed,
IsDepartment82Confirmed = @IsDepartment82Confirmed
where Id = @Id", item);

        public void UpdateParentId(Card item, int? newParentId) => conn.Execute(
@"update CRCards
set ParentId = @NewParentId
where Id = @Id",
new { Id = item.Id, NewParentId = newParentId });

        public void UpdateParentId2(Card item, int? newParentId) => conn.Execute(
@"update CRCards
set ParentId2 = @NewParentId
where Id = @Id",
new { Id = item.Id, NewParentId = newParentId });

        public void Delete(int cardId) => conn.Execute(
"delete from CRCards where Id = @Id",
new { Id = cardId });

        public bool IsConfirmed(int cardId, int cardConfirmationObjectId)
        {
            int count = conn.ExecuteScalar<int>(
@"select count(*) from CRCardConfirmations where CardId = @CardId and CardConfirmationObjectId = @CardConfirmationObjectId",
new { CardId = cardId, CardConfirmationObjectId = cardConfirmationObjectId });

            return count > 0;
        }

        public Card GetParentForInstalledProductCard(Card item) => conn.Query<Card>(
@"select * from CRCards where Id in
(
	select ParentId2 from CRCards where Id = @Id and ParentId2 is not null
)", item).FirstOrDefault();

        public Card GetParentForDismantledProductCard(Card item) => conn.Query<Card>(
@"select * from CRCards where Id in
(
	select ParentId from CRCards where Id = @Id and ParentId is not null
)", item).FirstOrDefault();

        public bool AreThereChildProductCards(Card item) => conn.ExecuteScalar<bool>(
@"select case when (select count(*) from CRCards where ParentId = @Id or ParentId2 = @Id) > 0 then 1 else 0 end", item);

        public Card FindByNumber(string number) => conn.Query<Card>(
@"select * from CRCards where Number = @Number",
new { Number = number }).FirstOrDefault();

        public bool IsCardWithActNumber(Card card) => conn.ExecuteScalar<bool>(
@"select case when count(*) > 0 then 1 else 0 end from CRCards where ActNumber = @ActNumber and @Id <> @Id", card);

    }
}

using Dapper;
using RepairCardsDapperData.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepairCardsDapperData.Data
{
    public class RequestRepo : RepoBase
    {
        public Request Get(int id) => conn.Query<Request, RepairType, Request>(
@"select r.*, rt.* from CRRequests r
left join CRRepairTypes rt on rt.Id = r.RepairTypeId
where r.Id = @Id",
(r, rt) => { r.RepairType = rt; return r; },
new { Id = id }).Single();

        public IEnumerable<Request> GetAll() => conn.Query<Request, RepairType, Request>(
@"select r.*, rt.* from CRRequests r
left join CRRepairTypes rt on rt.Id = r.RepairTypeId
order by (case when ConstructorConfirmDate is null then 0 else 1 end), Date desc", 
(r, rt) => { r.RepairType = rt; return r; });

        public int Add(Request item) => conn.ExecuteScalar<int>(
@"insert into CRRequests
(Department, Date, CardNumber, ProductCode, ProductName, ProductFactoryNumber, RepairTypeId, ShortOrder, 
RepairCode, RepairName, RepairOrder, RepairDirection, RepairCipher, RepairClientOrder, Constructor, ConstructorConfirmDate, ContractNumber)
values
(@Department, getdate(), @CardNumber, @ProductCode, @ProductName, @ProductFactoryNumber, @RepairTypeId, @ShortOrder, 
@RepairCode, @RepairName, @RepairOrder, @RepairDirection, @RepairCipher, @RepairClientOrder, @Constructor, @ConstructorConfirmDate, @ContractNumber);
select scope_identity();", item);

        public void Update(Request item) => conn.Execute(
@"update CRRequests 
set
Department = @Department,
Date = @Date,
CardNumber = @CardNumber,
ProductCode = @ProductCode,
ProductName = @ProductName,
ProductFactoryNumber = @ProductFactoryNumber,
RepairTypeId = @RepairTypeId,
ShortOrder = @ShortOrder,
RepairCode = @RepairCode,
RepairName = @RepairName,
RepairOrder = @RepairOrder,
RepairDirection = @RepairDirection,
RepairCipher = @RepairCipher,
RepairClientOrder = @RepairClientOrder,
Constructor = @Constructor,
ConstructorConfirmDate = 
(case when Constructor = 0 and @Constructor = 1 then getdate() 
when Constructor = 1 and @Constructor = 1 then ConstructorConfirmDate
else null end),
ContractNumber = @ContractNumber
where Id = @Id", item);

        public void Delete(int id) => conn.Execute(
"delete from CRRequests where Id = @Id",
new { Id = id });

        public bool CardIsConfirmedAndProductCodesAreTheSame(Card item) => conn.ExecuteScalar<bool>(
@"select case when count(*) > 0 then 1 else 0 end from CRRequests 
where 
CardNumber = @Number 
and RepairCode = @ProductCode 
and Constructor = 1", item);

        public bool IsThereRequestForSuchCard(Request item) => conn.ExecuteScalar<bool>(
@"select case when count(*) > 0 then 1 else 0 end from CRRequests where CardNumber = @CardNumber and Id <> @Id", item);
        
    }
}

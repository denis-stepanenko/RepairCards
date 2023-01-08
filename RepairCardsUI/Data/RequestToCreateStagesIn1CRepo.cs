using Dapper;
using RepairCardsDapperData.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepairCardsDapperData.Data
{
    public class RequestToCreateStagesIn1CRepo : RepoBase
    {
        public RequestToCreateStagesIn1C Get(int id) => conn.Query<RequestToCreateStagesIn1C>(
"select * from CRRequestsToCreateStagesIn1C where Id = @Id", new { Id = id }).FirstOrDefault();

        public IEnumerable<RequestToCreateStagesIn1C> GetAll() => conn.Query<RequestToCreateStagesIn1C>(
"select * from CRRequestsToCreateStagesIn1C order by Id desc");

        public int Add(RequestToCreateStagesIn1C item) => conn.ExecuteScalar<int>(
@"insert into CRRequestsToCreateStagesIn1C
(CardNumber, Department, CustomerName, ExecutorName, Date, CloseDate)
values
(@CardNumber, @Department, @CustomerName, @ExecutorName, @Date, @CloseDate);
select scope_identity();", item);

        public void Update(RequestToCreateStagesIn1C item) => conn.Execute(
@"update CRRequestsToCreateStagesIn1C
set
CardNumber = @CardNumber,
Department = @Department,
CustomerName = @CustomerName,
ExecutorName = @ExecutorName,
Date = @Date,
CloseDate = @CloseDate
where Id = @Id", item);

        public void Remove(RequestToCreateStagesIn1C item) => conn.Execute(
@"delete from CRRequestsToCreateStagesIn1C where Id = @Id", item);

        public void Confirm(int id, string userName, DateTime? date) => conn.Execute(
@"update CRRequestsToCreateStagesIn1C
set 
ExecutorName = @ExecutorName, 
CloseDate = @CloseDate
where Id = @Id",
new { Id = id, ExecutorName = userName, CloseDate = date });

        public bool AreThereRequestsWithCardNumber(string cardNumber) => conn.ExecuteScalar<bool>(
@"select case when exists (select * from CRRequestsToCreateStagesIn1C where CardNumber = @CardNumber) then 1 else 0 end", 
new { CardNumber = cardNumber });

    }
}

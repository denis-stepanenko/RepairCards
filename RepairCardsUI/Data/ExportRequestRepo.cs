using Dapper;
using RepairCardsDapperData.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepairCardsDapperData.Data
{
    public class ExportRequestRepo : RepoBase
    {
        public ExportRequest Get(int id) => conn.Query<ExportRequest>(
@"select * from CRExportRequests where Id = @Id", new { Id = id }).FirstOrDefault();

        public IEnumerable<ExportRequest> GetAll() => conn.Query<ExportRequest>(
"select * from CRExportRequests order by Id desc");

        public int Add(ExportRequest item) => conn.ExecuteScalar<int>(
@"insert into CRExportRequests
(CardNumber, Department, CustomerName, ExecutorName, Date, CloseDate, DeficitCreationDate)
values
(@CardNumber, @Department, @CustomerName, @ExecutorName, @Date, @CloseDate, @DeficitCreationDate);
select scope_identity();", item);

        public void Update(ExportRequest item) => conn.Execute(
@"update CRExportRequests
set
CardNumber = @CardNumber,
Department = @Department,
CustomerName = @CustomerName,
ExecutorName = @ExecutorName,
Date = @Date,
CloseDate = @CloseDate,
DeficitCreationDate = @DeficitCreationDate
where Id = @Id", item);

        public void Remove(ExportRequest item) => conn.Execute(
@"delete from CRExportRequests where Id = @Id", item);

        public void Confirm(int id, string userName, DateTime? date) => conn.Execute(
@"update CRExportRequests
set 
ExecutorName = @ExecutorName, 
CloseDate = @CloseDate
where Id = @Id",
new { Id = id, ExecutorName = userName, CloseDate = date });

        public void ConfirmDeficitCreation(int id, DateTime? date) => conn.Execute(
@"update CRExportRequests
set 
DeficitCreationDate = @Date
where Id = @Id",
new { Id = id, Date = date });

    }
}

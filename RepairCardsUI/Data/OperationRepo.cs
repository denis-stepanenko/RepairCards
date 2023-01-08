using Dapper;
using RepairCardsDapperData.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepairCardsDapperData.Data
{
    public class OperationRepo : RepoBase
    {
        public Operation Get(int id) => conn.Query<Operation>(
"select * from CROperations where Id = @Id",
new { Id = id }).FirstOrDefault();

        public Operation FindByCode(string code) => conn.Query<Operation>(
"select * from CROperations where Code = @Code",
new { Code = code }).FirstOrDefault();


        public IEnumerable<Operation> GetAll() => conn.Query<Operation>(
"select * from CROperations");

        public IEnumerable<Operation> GetAllActive() => conn.Query<Operation>(
@"select * from CROperations
where IsInactive <> 1");

        public IEnumerable<Operation> GetAllActiveByDepartment(int department) => conn.Query<Operation>(
@"select * from CROperations 
where Department = @Department and IsInactive <> 1",
new { Department = department });

        public int Add(Operation item) => conn.ExecuteScalar<int>(
@"insert into CROperations
(Code, [Name], Labor, Price, GroupName, Department, UnitName, [Description], IsInactive) 
values (@Code, @Name, @Labor, @Price, @GroupName, @Department, @UnitName, @Description, @IsInactive);
select scope_identity();", item);

        public void Update(Operation item) => conn.Execute(
@"update CROperations
set
Code = @Code,
[Name] = @Name,
Labor = @Labor,
Price = @Price,
GroupName = @GroupName,
Department = @Department,
UnitName = @UnitName,
[Description] = @Description,
IsInactive = @IsInactive
where Id = @Id", item);

        public void Delete(int id) => conn.Execute(
"delete from CROperations where Id = @Id",
new { Id = id });

        public string GenerateOperationNumber() => conn.Query<string>(
@"declare @number int = (select isnull(max(convert(int, Code)), 0)+1 from CROperations);
select replicate('0', 6 - len(@number)) + cast(@number as varchar);").Single();

        public bool IsThereOperationWithSuchCode(Operation item) => conn.ExecuteScalar<bool>(
@"select * from CROperations where Code = @Code and Id <> @Id", item);

        public bool IsOperationAlreadyUsedInTemplates(int id) => conn.ExecuteScalar<bool>(
@"select case when count(*) > 0 then 1 else 0 end from CRTemplateOperations where OperationId = @Id",
new { Id = id });
    }
}

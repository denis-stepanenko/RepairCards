using Dapper;
using RepairCardsDapperData.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepairCardsDapperData.Data
{
    public class ExecutorRepo : RepoBase
    {
        public Executor Get(int id) => conn.Query<Executor>(
"select * from CRExecutors where Id = @Id",
new { Id = id }).FirstOrDefault();

        public IEnumerable<Executor> GetAll() => conn.Query<Executor>(
"select * from CRExecutors");

        public IEnumerable<Executor> GetAllByDepartment(int department) => conn.Query<Executor>(
"select * from CRExecutors where Department = @Department",
new { Department = department });

        public int Add(Executor item) => conn.ExecuteScalar<int>(
@"insert into CRExecutors
([Name], Department) values (@Name, @Department)
select scope_identity();", item);

        public void Update(Executor item) => conn.Execute(
@"update CRExecutors
set
[Name] = @Name,
Department = @Department
where Id = @Id", item);

        public void Delete(int id) => conn.Execute(
"delete from CRExecutors where Id = @Id",
new { Id = id });

        public bool IsExecuterAlreadyUsedInOperations(int id) => conn.ExecuteScalar<bool>(
@"select case when count(*) > 0 then 1 else 0 end from CRCardOperations where ExecutorId = @Id",
new { Id = id });

    }
}

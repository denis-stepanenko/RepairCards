using Dapper;
using RepairCardsDapperData.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepairCardsDapperData.Data
{
    public class RoleRepo : RepoBase
    {
        public Role Get(int id) => conn.Query<Role>(
"select * from CRRoles where Id = @Id",
new { Id = id }).FirstOrDefault();

        public IEnumerable<Role> GetAll() => conn.Query<Role>(
"select * from CRRoles");

        public int Add(Role item) => conn.ExecuteScalar<int>(
@"insert into CRRoles
([Name]) values (@Name)
select scope_identity();", item);

        public void Update(Role item) => conn.Execute(
@"update CRRoles
set
[Name] = @Name
where Id = @Id", item);

        public void Delete(int id) => conn.Execute(
"delete from CRRoles where Id = @Id",
new { Id = id });
        
    }
}

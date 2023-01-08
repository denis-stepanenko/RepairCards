using Dapper;
using RepairCardsDapperData.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepairCardsDapperData.Data
{
    public class TemplateRepo : RepoBase
    {
        public Template Get(int id) => conn.Query<Template>(
"select * from CRTemplates where Id = @Id",
new { Id = id }).Single();

        public IEnumerable<Template> GetAll() => conn.Query<Template>(
"select * from CRTemplates");

        public IEnumerable<Template> GetAllByDepartment(int department) => conn.Query<Template>(
"select * from CRTemplates where Department = @Department",
new { Department = department });

        public int Add(Template item) => conn.ExecuteScalar<int>(
@"insert into CRTemplates
(Department, [Name], Description) values (@Department, @Name, @Description)
select scope_identity();", item);

        public void Update(Template item) => conn.Execute(
@"update CRTemplates
set
Department = @Department,
[Name] = @Name,
Description = @Description
where Id = @Id", item);

        public void Delete(int id) => conn.Execute(
"delete from CRTemplates where Id = @Id",
new { Id = id });
        
    }
}

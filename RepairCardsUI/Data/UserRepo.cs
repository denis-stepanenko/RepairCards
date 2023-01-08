using Dapper;
using RepairCardsDapperData.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RepairCardsDapperData.Data
{
    public class UserRepo : RepoBase
    {
        public IEnumerable<User> GetAll() => conn.Query<User, Role, User>(
@"select u.Id, u.Name, u.Department, u.RoleId, 
cast(DecryptByPassPhrase('m14780', u.[Password]) as varchar(max)) Password, r.* from CRUsers u
left join CRRoles r on r.Id = u.RoleId
order by u.Name",
(u, r) => { u.Role = r; return u; });

        public User Get(int id) => conn.Query<User, Role, User>(
@"select u.Id, u.Name, u.Department, u.RoleId, 
cast(DecryptByPassPhrase('m14780', u.[Password]) as varchar(max)) Password, r.* from CRUsers u
left join CRRoles r on r.Id = u.RoleId
where u.Id = @Id",
(u, r) => { u.Role = r; return u; },
new { Id = id }).Single();

        public bool IsCorrectPassword(int userId, string password)
        {
            var parameters = new DynamicParameters();
            parameters.Add("UserId", userId);
            parameters.Add("Password", password, DbType.String, ParameterDirection.Input, 20);
            parameters.Add("Result", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            conn.Execute("CRCheckPassword", parameters, commandType: CommandType.StoredProcedure);
            return parameters.Get<bool>("Result");
        }

        public int Add(User item) => conn.ExecuteScalar<int>(
@"insert into CRUsers
([Name], [Password], [Department], RoleId)
values
(@Name, EncryptByPassPhrase('m14780', @Password), @Department, @RoleId);
select scope_identity();",
new
{
    Id = item.Id,
    Name = item.Name,
    Password = new DbString { Value = item.Password, IsAnsi = true },
    Department = item.Department,
    RoleId = item.RoleId
});

        public void Update(User item) => conn.Execute(
@"update CRUsers
set
[Name] = @Name,
[Password] = EncryptByPassPhrase('m14780', @Password),
Department = @Department,
RoleId = @RoleId
where Id = @Id", 
new 
{ 
    Id = item.Id,
    Name = item.Name,
    Password = new DbString { Value = item.Password, IsAnsi = true },
    Department = item.Department,
    RoleId = item.RoleId
});

        public void Delete(int id) => conn.Execute(
"delete from CRUsers where Id = @Id",
new { Id = id });

    }
}

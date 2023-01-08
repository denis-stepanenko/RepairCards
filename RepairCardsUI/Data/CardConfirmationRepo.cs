using Dapper;
using RepairCardsDapperData.Models;
using System.Collections.Generic;

namespace RepairCardsDapperData.Data
{
    public class CardConfirmationRepo : RepoBase
    {
        public IEnumerable<CardConfirmation> GetAllByCard(int cardId) => conn.Query<CardConfirmation, CardConfirmationObject, Role, CardConfirmation>(
@"select c.*, co.*, r.* from CRCardConfirmations c 
left join CRCardConfirmationObjects co on co.Id = c.CardConfirmationObjectId
left join CRRoles r on r.Id = c.UserRoleId
where c.CardId = @CardId",
(c, co, r) => { c.CardConfirmationObject = co; c.UserRole = r; return c; },
new { CardId = cardId });

        public int Add(CardConfirmation item) => conn.ExecuteScalar<int>(
@"insert into CRCardConfirmations
(CardId, UserId, UserRoleId, CardConfirmationObjectId, UserName, Date)
values
(@CardId, @UserId, @UserRoleId, @CardConfirmationObjectId, @UserName, getdate());
select scope_identity();", item);

        public void Delete(int id) => conn.Execute(
"delete from CRCardConfirmations where Id = @Id",
new { Id = id });

        public bool IsConfirmed(int cardId, int cardConfirmationObjectId)
        {
            int count = conn.ExecuteScalar<int>(
@"select count(*) from CRCardConfirmations where CardId = @CardId and CardConfirmationObjectId = @CardConfirmationObjectId",
new { CardId = cardId, CardConfirmationObjectId = cardConfirmationObjectId });

            return count > 0;
        }
    }
}

using Dapper;
using RepairCardsDapperData.Models;
using System.Linq;

namespace RepairCardsDapperData.Data
{
    public class CardDetailsRepo : RepoBase
    {
        public CardDetails Get(int cardId) => conn.Query<CardDetails>(
"select * from CRCardDetails where Id = @Id", 
new { Id = cardId }).FirstOrDefault();

        public void Update(CardDetails item) => conn.Execute(
@"update CRCardDetails
set 
ExternalDefects = @ExternalDefects,
InternalDefects = @InternalDefects,
Malfunctions = @Malfunctions,
CauseOfProductFailure = @CauseOfProductFailure,
ScopeOfRepair = @ScopeOfRepair,
CommissionReport = @CommissionReport
where Id = @Id", item);
    }
}

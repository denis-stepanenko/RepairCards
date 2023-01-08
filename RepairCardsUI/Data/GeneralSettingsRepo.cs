using Dapper;
using RepairCardsDapperData.Models;
using System.Linq;

namespace RepairCardsDapperData.Data
{
    public class GeneralSettingsRepo : RepoBase
    {
        public GeneralSettings Get() => conn.Query<GeneralSettings>(
@"select * from CRGeneralSettings").FirstOrDefault();

        public void Update(GeneralSettings item) => conn.Execute(
@"update CRGeneralSettings 
set
PeriodClosingDay = @PeriodClosingDay", item);
        
    }
}

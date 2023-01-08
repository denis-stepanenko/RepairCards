using Dapper;
using RepairCardsDapperData.Models;
using System.Collections.Generic;

namespace RepairCardsDapperData.Data
{
    public class SalariedEmployeeLaborCoefficientRepo : RepoBase
    {
        public IEnumerable<SalariedEmployeeLaborCoefficient> GetAll() => conn.Query<SalariedEmployeeLaborCoefficient>(
@"select * from SalariedEmployeeLaborCoefficients 
order by Date desc");

    }
}

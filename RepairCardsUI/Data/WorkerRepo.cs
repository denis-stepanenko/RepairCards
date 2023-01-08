using Dapper;
using RepairCardsDapperData.Models;
using System.Collections.Generic;

namespace RepairCardsDapperData.Data
{
    public class WorkerRepo : RepoBase
    {
        public IEnumerable<Worker> GetAll() => conn.Query<Worker>(
"select famalio Name from TempKadry");
    }
}

using Dapper;
using RepairCardsDapperData.Models;
using System.Collections.Generic;

namespace RepairCardsDapperData.Data
{
    public class CardConfirmationObjectRepo : RepoBase
    {
        public IEnumerable<CardConfirmationObject> GetAll() => conn.Query<CardConfirmationObject>(
"select * from CRCardConfirmationObjects");
    }
}

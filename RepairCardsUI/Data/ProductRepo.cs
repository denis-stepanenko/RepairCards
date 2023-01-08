using Dapper;
using RepairCardsDapperData.Models;
using System.Collections.Generic;

namespace RepairCardsDapperData.Data
{
    public class ProductRepo : RepoBase
    {
        public IEnumerable<Product> GetAll() => conn.Query<Product>(
@"select Id, 2 TableId, Decnum Code, Name from ref_dse
union				  
select Id, 1 TableId, DecNum Code, Name from ref_purchase");

    }
}

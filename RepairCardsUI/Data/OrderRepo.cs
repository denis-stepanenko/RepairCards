using Dapper;
using RepairCardsDapperData.Models;
using System.Collections.Generic;
using System.Data;

namespace RepairCardsDapperData.Data
{
    public class OrderRepo : RepoBase
    {
        public IEnumerable<Order> GetAllByProduct(string productCode) => conn.Query<Order>(
"CROrders",
new { ProductCode = productCode }, commandType: CommandType.StoredProcedure);


        public IEnumerable<Order> GetAll() => conn.Query<Order>(
@"select distinct Direction, Cipher, SpecificationType from v_compacted_orders
where Direction is not null");

        public int? GetSpecificationType(string direction) => conn.ExecuteScalar<int?>(
@"select distinct SpecificationType from v_compacted_orders
where isnull(Cipher, Direction) = @Direction", new { Direction = direction });
    }
}

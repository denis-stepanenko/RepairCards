using Dapper;
using RepairCardsDapperData.Models;
using System.Collections.Generic;

namespace RepairCardsDapperData.Data
{
    public class MaterialRepo : RepoBase
    {
        public IEnumerable<Material> GetAll() => conn.Query<Material, MaterialUnit, Material>(
@"select m.MaterialId Id, m.Code, m.Name, m.Size, m.Type, m.Price, m.UnitId, u.UnitId Id, u.Code, u.Name, u.FullName 
from tMaterial m join tUnit u on m.UnitId = u.UnitId",
(m, u) => { m.Unit = u; return m; });

    }
}

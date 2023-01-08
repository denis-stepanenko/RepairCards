using Dapper;
using RepairCardsDapperData.Models;
using RepairCardsUI.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace RepairCardsDapperData.Data
{
    public class ProductMaterialRepo : RepoBase
    {
        public IEnumerable<ProductMaterial> GetAll(string productCode) => conn.Query<ProductMaterial, MaterialUnit, ProductMaterial>(
"s_PP_export_mat_1c_newgeneration_cardrepair",
(m, u) => { m.Unit = u; return m; },
new 
{ 
    dse_num = productCode, 
    count = 1,  
    vr = "cr" + Environment.UserName + DateTime.Now.ToString("ddMMyyhhmmssffff"),
    flag = 1,
    type_det = 1,
    cur_cex = 0
},
commandType: CommandType.StoredProcedure);

    }
}

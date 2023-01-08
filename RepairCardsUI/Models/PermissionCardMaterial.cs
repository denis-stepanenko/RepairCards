using System.ComponentModel.DataAnnotations.Schema;

namespace RepairCardsUI.Models
{
    [Table("CRPermissionCardMaterials")]
    public class PermissionCardMaterial
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public decimal Count { get; set; }
        public decimal Price { get; set; }
        public int? Department { get; set; }
        public int UnitId { get; set; }

        public int PermissionCardId { get; set; }
        public virtual PermissionCard Card { get; set; }
    }
}

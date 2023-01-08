using System.ComponentModel.DataAnnotations.Schema;

namespace RepairCardsUI.Models
{
    [Table("CRPermissionCardOperations")]
    public class PermissionCardOperation
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal Labor { get; set; }
        public string UnitName { get; set; }
        public string GroupName { get; set; }
        public int Department { get; set; }
        public decimal Price { get; set; }

        public int PermissionCardId { get; set; }
        public virtual PermissionCard Card { get; set; }

        [NotMapped]
        public decimal LaborWithCoefficient { get; set; }
    }
}

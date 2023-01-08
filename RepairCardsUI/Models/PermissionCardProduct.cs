using System.ComponentModel.DataAnnotations.Schema;

namespace RepairCardsUI.Models
{
    [Table("CRPermissionCardProducts")]
    public class PermissionCardProduct
    {
        public int Id { get; set; }
        public string Order { get; set; }
        public string Direction { get; set; }
        public string Stage { get; set; }
        public string Cipher { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

        public int PermissionCardId { get; set; }
        public virtual PermissionCard Card { get; set; }
    }
}

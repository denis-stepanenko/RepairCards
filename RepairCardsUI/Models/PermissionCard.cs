using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairCardsUI.Models
{
    [Table("CRPermissionCards")]
    [AddINotifyPropertyChangedInterface]
    public class PermissionCard
    {
        public PermissionCard()
        {
            Products = new ObservableCollection<PermissionCardProduct>();
            PurchasedProducts = new ObservableCollection<PermissionCardPurchasedProduct>();
            Materials = new ObservableCollection<PermissionCardMaterial>();
            Operations = new ObservableCollection<PermissionCardOperation>();
        }

        public int Id { get; set; }
        public string Number { get; set; }
        public int? Department { get; set; }
        public string Recipient { get; set; }
        public DateTime? Date { get; set; }
        public string Cause { get; set; }
        public string Description { get; set; }
        public string ActionsToEliminateCauses { get; set; }
        public string ReplacementEffect { get; set; }
        public string Direction { get; set; }
        public string Cipher { get; set; }
        public int? OTKUserId { get; set; }
        public string OTKUsername { get; set; }
        public bool IsConfirmed => OTKUserId != null;

        public virtual ObservableCollection<PermissionCardProduct> Products { get; set; }
        public virtual ObservableCollection<PermissionCardPurchasedProduct> PurchasedProducts { get; set; }
        public virtual ObservableCollection<PermissionCardMaterial> Materials { get; set; }
        public virtual ObservableCollection<PermissionCardOperation> Operations { get; set; }
    }
}

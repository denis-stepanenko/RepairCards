using System;

namespace RepairCardsDapperData.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string ActNumber { get; set; }
        public string PermissionCardNumber { get; set; }

        public int Department { get; set; }
        public string Order { get; set; }
        public string Stage { get; set; }
        public string FactoryNumber { get; set; }
        public string Direction { get; set; }
        public string Cipher { get; set; }
        public string ClientOrder { get; set; }

        public int? ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }

        public string InvoiceNumber { get; set; }
        public DateTime? Date { get; set; }
        public int Source { get; set; }
        public string ReasonForRepair { get; set; }

        public int? RepairTypeId { get; set; }
        public RepairType RepairType { get; set; }

        public int? CardStatusId { get; set; }
        public CardStatus CardStatus { get; set; }

        public int? ParentId { get; set; }
        public int? ParentId2 { get; set; }
        public string ParentNumber { get; set; }

        public string CreatorName { get; set; }

        public bool IsConfirmed { get; set; }

        public bool IsDepartment4Confirmed { get; set; }
        public bool IsDepartment5Confirmed { get; set; }
        public bool IsDepartment6Confirmed { get; set; }
        public bool IsDepartment13Confirmed { get; set; }
        public bool IsDepartment17Confirmed { get; set; }
        public bool IsDepartment80Confirmed { get; set; }
        public bool IsDepartment82Confirmed { get; set; }
    }
}

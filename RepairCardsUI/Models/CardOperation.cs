using System;

namespace RepairCardsDapperData.Models
{
    public class CardOperation
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public int Number { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal Labor { get; set; }
        public int Type { get; set; }
        public DateTime? Date { get; set; }

        public decimal LaborAll { get; set; }
        public string UnitName { get; set; }
        public string GroupName { get; set; }
        public int Department { get; set; }

        public int? ExecutorId { get; set; }

        public Executor Executor { get; set; }

        public Card Card { get; set; }

        public string CardNumber => Card.Number;

        public string TemplateCode { get; set; }
        public string TemplateName { get; set; }

        public string Comment { get; set; }

        public string ConfirmUserName { get; set; }
        public int? ConfirmUserId { get; set; }

        public bool IsConfirmed => ConfirmUserId != null;
    }
}

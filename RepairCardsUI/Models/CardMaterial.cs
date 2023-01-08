namespace RepairCardsDapperData.Models
{
    public class CardMaterial
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public decimal Count { get; set; }
        public decimal Price { get; set; }
        public int? Department { get; set; }

        public int UnitId { get; set; }

        public MaterialUnit Unit { get; set; }

        public string UnitName => Unit.Name;

        public Card Card { get; set; }

        public string CardNumber => Card.Number;
    }
}

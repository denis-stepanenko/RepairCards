namespace RepairCardsDapperData.Models
{
    public class CardRepairProduct
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }
}

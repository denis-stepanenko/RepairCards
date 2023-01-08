namespace RepairCardsDapperData.Models
{
    public class Order
    {
        public string Number { get; set; }
        public string Direction { get; set; }
        public string Cipher { get; set; }
        public string ClientOrder { get; set; }
        public int SpecificationType { get; set; }
    }
}

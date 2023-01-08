namespace RepairCardsDapperData.Models
{
    public class Operation
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Labor { get; set; }
        public decimal Price { get; set; }
        public string GroupName { get; set; }
        public int Department { get; set; }
        public string UnitName { get; set; }
        public string Description { get; set; }
        public bool IsInactive { get; set; }
    }
}

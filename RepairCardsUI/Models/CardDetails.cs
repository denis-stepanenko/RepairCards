namespace RepairCardsDapperData.Models
{
    public class CardDetails
    {
        public int Id { get; set; }
        public string ExternalDefects { get; set; }
        public string InternalDefects { get; set; }
        public string Malfunctions { get; set; }
        public string CauseOfProductFailure { get; set; }
        public string ScopeOfRepair { get; set; }
        public string CommissionReport { get; set; }
    }
}

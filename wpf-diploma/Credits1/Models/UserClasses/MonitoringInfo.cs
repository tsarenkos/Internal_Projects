namespace Credits1.Models.UserClasses
{
    public class MonitoringInfo
    {
        public string ClientName { get; set; }
        public string Collateral_agreement { get; set; }           
        public int TypeId { get; set; }
        public string Description { get; set; }               
        
        public System.DateTime Previous_date { get; set; }
        public System.DateTime Planned_date { get; set; }
        public string Note { get; set; }

        public string EmployeeSupport { get; set; }
        public string EmployeeMonitoring { get; set; }
    }
}

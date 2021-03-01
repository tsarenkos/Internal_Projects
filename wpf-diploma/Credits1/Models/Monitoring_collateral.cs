using System.Collections.Generic;

namespace Credits1.Models
{
    public partial class Monitoring_collateral
    {
        public Monitoring_collateral()
        {
            this.Employees = new List<Employee>();
        }

        public string Collateral_agreement { get; set; }
        public System.DateTime Previous_date { get; set; }
        public System.DateTime Planned_date { get; set; }
        public string Note { get; set; }
        public virtual Collateral Collateral { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}

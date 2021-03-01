using System.Collections.Generic;

namespace Credits1.Models
{
    public partial class Employee
    {
        public Employee()
        {
            this.Monitoring_collateral = new List<Monitoring_collateral>();
        }

        public int Employee_Id { get; set; }
        public string Name { get; set; }
        public int Department_Id { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Monitoring_collateral> Monitoring_collateral { get; set; }
        
    }
}

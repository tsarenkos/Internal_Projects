using System.Collections.Generic;

namespace Credits1.Models
{
    public partial class Department
    {
        public Department()
        {
            this.Employees = new List<Employee>();
        }

        public int Department_Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiApp.DAL.Entities
{
    public partial class Deliverer
    {
        public Deliverer()
        {
            Machines = new HashSet<Machine>();
        }

        public int DelivererId { get; set; }
        public string DelivererName { get; set; }

        public virtual ICollection<Machine> Machines { get; set; }
    }
}

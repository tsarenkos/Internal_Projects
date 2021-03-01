using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiApp.DAL.Entities
{
    public partial class Machine
    {
        public Machine()
        {
            Breakages = new HashSet<Breakage>();
            Requests = new HashSet<Request>();
        }

        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public float Price { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public int DelivererId { get; set; }

        public virtual Deliverer Deliverer { get; set; }
        public virtual ICollection<Breakage> Breakages { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}

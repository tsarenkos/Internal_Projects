using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Factory.Storage.Core.Entities
{
    public class Machine
    {
        public int MachineId { get; set; }        
        public string MachineName { get; set; }
        public float Price { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public int DelivererId { get; set; }

        [ForeignKey("DelivererId")]
        public Deliverer Deliverer { get; set; }
        public List<Breakage> Breakages { get; set; }
        public List<Request> Requests { get; set; }
    }
}

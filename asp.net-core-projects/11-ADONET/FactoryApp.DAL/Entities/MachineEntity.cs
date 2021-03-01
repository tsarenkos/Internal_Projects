using System;

namespace FactoryApp.DAL.Entities
{
    public class MachineEntity
    {
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public float Price { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public int DelivererId { get; set; }
    }
}

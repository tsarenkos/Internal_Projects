using System;

namespace FactoryApp.BLL.BusinessModels
{
    public class MachineModel
    {
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public float Price { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public string DelivererName { get; set; }
    }
}

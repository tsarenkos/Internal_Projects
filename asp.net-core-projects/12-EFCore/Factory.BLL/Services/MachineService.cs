using Factory.BLL.BusinessModels;
using Factory.BLL.Interfaces;
using Factory.DAL;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace Factory.BLL.Services
{
    public class MachineService:IMachineService
    {        
        public List<MachineModel> GetAllMachines()
        {
            List<MachineModel> machineModels = new List<MachineModel>();
            using(FactoryContext context = new FactoryContext())
            {
                var machines = context.Machines.Include(m => m.Deliverer).ToList();
                foreach(var item in machines)
                {
                    MachineModel machine = new MachineModel();
                    machine.MachineId = item.MachineId;
                    machine.MachineName = item.MachineName;
                    machine.Price = item.Price;
                    machine.DateOfDelivery = item.DateOfDelivery;
                    machine.DelivererName = item.Deliverer.DelivererName;
                    machineModels.Add(machine);
                }
            }
            return machineModels;
        }
    }
}

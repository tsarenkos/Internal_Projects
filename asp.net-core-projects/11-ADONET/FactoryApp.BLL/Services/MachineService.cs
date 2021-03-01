using FactoryApp.BLL.BusinessModels;
using FactoryApp.BLL.Interfaces;
using FactoryApp.DAL.Entities;
using FactoryApp.DAL.Interfaces;
using System.Collections.Generic;


namespace FactoryApp.BLL.Services
{
    public class MachineService:IMachineService
    {
        private readonly IRepository<MachineEntity> repository;
        private readonly IRepository<DelivererEntity> delivererRepository;

        public MachineService(IRepository<MachineEntity> repository, IRepository<DelivererEntity> delivererRepository)
        {
            this.repository = repository;
            this.delivererRepository = delivererRepository;
        }

        public List<MachineModel> GetAllMachines()
        {            
            var machines = repository.GetAll();
            if (machines == null)
            {
                return null;
            }
            List<MachineModel> machineModels = new List<MachineModel>();
            foreach (var item in machines)
            {
                MachineModel machine = new MachineModel();
                machine.MachineId = item.MachineId;
                machine.MachineName = item.MachineName;
                machine.Price = item.Price;
                machine.DateOfDelivery = item.DateOfDelivery;
                machine.DelivererName = delivererRepository.GetById(item.DelivererId)?.DelivererName;
                machineModels.Add(machine);
            }
            return machineModels;
        }
    }
}

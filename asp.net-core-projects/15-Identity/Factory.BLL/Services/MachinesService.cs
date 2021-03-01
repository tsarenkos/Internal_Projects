using Factory.BLL.BusinessModels;
using Factory.BLL.Interfaces;
using Factory.Storage.Core.Entities;
using Factory.Storage.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Factory.BLL.Services
{
    public class MachinesService: IMachinesService
    {
        private readonly IUnitOfWork unitOfWork;
        public MachinesService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        
        public List<MachineModel> GetAllMachines()
        {
            var machines = unitOfWork.Machines.GetAll();
            if (machines == null)
            {
                return null;
            }

            List<MachineModel> machineModels = new List<MachineModel>();            
            foreach(var item in machines)
            {
                MachineModel machine = new MachineModel();
                machine.MachineId = item.MachineId;
                machine.MachineName = item.MachineName;
                machine.Price = item.Price;
                machine.DateOfDelivery = item.DateOfDelivery;
                machine.DelivererName = unitOfWork.Deliverers.GetById(item.DelivererId)?.DelivererName;
                machineModels.Add(machine);
            }                        
            return machineModels;            
        }

        public void AddMachine(MachineModel machineModel)
        {
            try
            {
                if (machineModel == null)
                {
                    throw new ArgumentNullException();
                }

                Machine machine = new Machine();
                machine.MachineName = machineModel.MachineName;
                machine.Price = machineModel.Price;
                machine.DateOfDelivery = machineModel.DateOfDelivery;

                Deliverer delivererMachine = unitOfWork.Deliverers.GetAll().FirstOrDefault(d => d.DelivererName == machineModel.DelivererName);
                if (delivererMachine != null)
                    machine.DelivererId = delivererMachine.DelivererId;

                unitOfWork.Machines.Create(machine);
                unitOfWork.SaveChanges();
            }
            catch (Exception exc)
            {
                throw exc;
            }                        
        }

        public MachineModel GetById(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            Machine machine = unitOfWork.Machines.GetById(id);
            if (machine == null)
            {
                return null;
            }
            Deliverer delivererMachine = unitOfWork.Deliverers.GetById(machine.DelivererId);
            MachineModel machineModel = new MachineModel
            {
                MachineId = machine.MachineId,
                MachineName = machine.MachineName,
                Price = machine.Price,
                DateOfDelivery = machine.DateOfDelivery,
                DelivererName = delivererMachine?.DelivererName
            };
            return machineModel;
        }
    }
}

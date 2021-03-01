using Factory.BLL.BusinessModels;
using Factory.BLL.Interfaces;
using Factory.Storage.Core.Entities;
using Factory.Storage.Core.Interfaces;
using System;
using System.Collections.Generic;


namespace Factory.BLL.Services
{
    public class FactoryService:IFactoryService
    {
        private readonly IUnitOfWork unitOfWork;
        public FactoryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public List<EmployeeModel> GetAllEmployees()
        {
            List<EmployeeModel> employeeModels = new List<EmployeeModel>();

            var employees = unitOfWork.Employees.GetAll();
            foreach (var item in employees)
            {
                EmployeeModel emp = new EmployeeModel();
                emp.EmployeeId = item.EmployeeId;
                emp.Name = item.Name;
                emp.Position = item.Position;
                employeeModels.Add(emp);
            }
            
            return employeeModels;
        }
        public List<MachineModel> GetAllMachines()
        {
            List<MachineModel> machineModels = new List<MachineModel>();

            var machines = unitOfWork.Machines.GetAll();
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

        public void AddDeliverer(DelivererModel delivererModel)
        {
            if (delivererModel == null)
            {
                throw new ArgumentNullException();
            }

            Deliverer deliverer = new Deliverer();
            deliverer.DelivererName = delivererModel.DelivererName;

            unitOfWork.Deliverers.Create(deliverer);
            unitOfWork.SaveChanges();            
        }

        public void AddRequest(RequestModel requestModel)
        {
            if (requestModel == null)
            {
                throw new ArgumentNullException();
            }

            Request request = new Request();
            request.RequestCreatorId = requestModel.RequestCreatorId;
            request.RequestHandlerId = requestModel.RequestHandlerId;
            request.MachineId = requestModel.MachineId;
            request.DateOfCreate = DateTime.Now;
            request.RequestStatusId = 1;
            request.InnerRequestId = requestModel.InnerRequestId;

            unitOfWork.Requests.Create(request);
            unitOfWork.SaveChanges();           
        }

        public void AddRequestHandler(int requestId, int employeeId)
        {
            if(requestId == 0 || employeeId == 0)
            {
                throw new ArgumentNullException();
            }
            Request request = unitOfWork.Requests.GetById(requestId);
            if (request != null)
            {
                request.RequestHandlerId = employeeId;
                unitOfWork.Requests.Update(request);
                unitOfWork.SaveChanges();
            }                             
        }
    }
}

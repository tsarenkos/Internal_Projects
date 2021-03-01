using Factory.BLL.BusinessModels;
using System.Collections.Generic;


namespace Factory.BLL.Interfaces
{
    public interface IFactoryService
    {
        List<EmployeeModel> GetAllEmployees();
        List<MachineModel> GetAllMachines();
        void AddDeliverer(DelivererModel delivererModel);
        void AddRequest(RequestModel requestModel);
        void AddRequestHandler(int requestId, int employeeId);
    }
}

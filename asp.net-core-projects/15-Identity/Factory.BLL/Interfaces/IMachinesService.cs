using Factory.BLL.BusinessModels;
using System.Collections.Generic;


namespace Factory.BLL.Interfaces
{
    public interface IMachinesService
    {
        List<MachineModel> GetAllMachines();
        MachineModel GetById(int id);
        void AddMachine(MachineModel machineModel);        
    }
}

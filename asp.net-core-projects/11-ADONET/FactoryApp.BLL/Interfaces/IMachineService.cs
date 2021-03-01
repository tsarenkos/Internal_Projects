using FactoryApp.BLL.BusinessModels;
using System.Collections.Generic;


namespace FactoryApp.BLL.Interfaces
{
    public interface IMachineService
    {
        List<MachineModel> GetAllMachines();
    }
}

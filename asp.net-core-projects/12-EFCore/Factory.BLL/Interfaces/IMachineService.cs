using Factory.BLL.BusinessModels;
using System.Collections.Generic;


namespace Factory.BLL.Interfaces
{
    public interface IMachineService
    {       
        List<MachineModel> GetAllMachines();        
    }
}

using FactoryApp.BLL.BusinessModels;
using System.Collections.Generic;


namespace FactoryApp.BLL.Interfaces
{
    public interface IEmployeeService
    {
        List<EmployeeModel> GetAllEmployees();
    }
}

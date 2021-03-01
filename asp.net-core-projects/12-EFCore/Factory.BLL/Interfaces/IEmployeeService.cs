using Factory.BLL.BusinessModels;
using System.Collections.Generic;


namespace Factory.BLL.Interfaces
{
    public interface IEmployeeService
    {
        List<EmployeeModel> GetAllEmployees();        
    }
}

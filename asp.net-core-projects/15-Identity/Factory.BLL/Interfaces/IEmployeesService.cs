using Factory.BLL.BusinessModels;
using System.Collections.Generic;


namespace Factory.BLL.Interfaces
{
    public interface IEmployeesService
    {
        List<EmployeeModel> GetAllEmployees();
        EmployeeModel GetById(int id);
        void AddEmployee(EmployeeModel employeeModel);        
    }
}

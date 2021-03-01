using Factory.BLL.BusinessModels;
using Factory.BLL.Interfaces;
using Factory.DAL;
using Factory.DAL.Entities;
using System.Collections.Generic;
using System.Linq;


namespace Factory.BLL.Services
{
    public class EmployeeService:IEmployeeService
    {
        public List<EmployeeModel> GetAllEmployees()
        {
            List<EmployeeModel> employeeModels = new List<EmployeeModel>();

            using(FactoryContext context = new FactoryContext())
            {
                List<Employee> employees = context.Employees.ToList(); 
                foreach(var item in employees)
                {
                    EmployeeModel emp = new EmployeeModel();
                    emp.EmployeeId = item.EmployeeId;
                    emp.Name = item.Name;
                    emp.Position = item.Position;
                    employeeModels.Add(emp);
                }
            }
            return employeeModels;
        }
    }
}

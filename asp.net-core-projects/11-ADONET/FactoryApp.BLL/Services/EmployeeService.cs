using FactoryApp.BLL.BusinessModels;
using FactoryApp.BLL.Interfaces;
using FactoryApp.DAL.Entities;
using FactoryApp.DAL.Interfaces;
using System.Collections.Generic;


namespace FactoryApp.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<EmployeeEntity> repository;

        public EmployeeService(IRepository<EmployeeEntity> repository)
        {
            this.repository = repository;
        }
        public List<EmployeeModel> GetAllEmployees()
        {
            var employees = repository.GetAll();
            if (employees == null)
            {
                return null;
            }

            List<EmployeeModel> employeeModels = new List<EmployeeModel>();
            
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
    }
}

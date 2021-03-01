using Factory.BLL.BusinessModels;
using Factory.BLL.Interfaces;
using Factory.Storage.Core.Entities;
using Factory.Storage.Core.Interfaces;
using System;
using System.Collections.Generic;


namespace Factory.BLL.Services
{
    public class EmployeesService:IEmployeesService
    {
        private readonly IUnitOfWork unitOfWork;
        public EmployeesService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public List<EmployeeModel> GetAllEmployees()
        {
            var employees = unitOfWork.Employees.GetAll();
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

        public void AddEmployee(EmployeeModel employeeModel)
        {
            try
            {
                if (employeeModel == null)
                {
                    throw new ArgumentNullException();
                }

                Employee employee = new Employee();
                employee.Name = employeeModel.Name;
                employee.Position = employeeModel.Position;

                unitOfWork.Employees.Create(employee);
                unitOfWork.SaveChanges();
            }
            catch (Exception exc)
            {
                throw exc;
            }                        
        }

        public EmployeeModel GetById(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            Employee employee = unitOfWork.Employees.GetById(id);
            if (employee == null)
            {
                return null;
            }
            EmployeeModel employeeModel = new EmployeeModel
            {
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                Position = employee.Position
            };
            return employeeModel;
        }
    }
}

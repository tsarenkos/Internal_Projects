using System;
using System.Collections.Generic;
using WebApiApp.BLL.BusinessModels;
using WebApiApp.BLL.Interfaces;
using WebApiApp.DAL.Entities;
using WebApiApp.DAL.Interfaces;
using WebApiApp.DAL.Repositories;

namespace WebApiApp.BLL.Services
{
    public class FactoryService : IFactoryService
    {
        private IRepository<Employee> repository;

        public FactoryService()
        {
            this.repository = new EmployeeRepository();
        }

        public void Create(EmployeeModel item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException();
                }
                Employee emp = new Employee();
                emp.EmployeeId = item.EmployeeId;
                emp.Name = item.Name;
                emp.Position = item.Position;

                repository.Create(emp);
            }
            catch (Exception exc)
            {
                throw exc;
            }            
        }

        public void Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException();
                }
                repository.Delete(id);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            
        }

        public EmployeeModel Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            Employee emp = repository.Get(id);

            if (emp == null)
            {
                return null;
            }

            EmployeeModel model = new EmployeeModel();
            model.EmployeeId = emp.EmployeeId;
            model.Name = emp.Name;
            model.Position = emp.Position;
            return model;
        }

        public List<EmployeeModel> GetAll()
        {
            IEnumerable<Employee> employees = repository.GetAll();

            if (employees == null)
            {
                return null;
            }

            List<EmployeeModel> models = new List<EmployeeModel>();

            foreach(Employee emp in employees)
            {
                EmployeeModel model = new EmployeeModel();
                model.EmployeeId = emp.EmployeeId;
                model.Name = emp.Name;
                model.Position = emp.Position;
                models.Add(model);
            }

            return models;
        }

        public void Update(EmployeeModel item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException();
                }
                Employee emp = repository.Get(item.EmployeeId);

                if (emp != null)
                {
                    emp.EmployeeId = item.EmployeeId;
                    emp.Name = item.Name;
                    emp.Position = item.Position;

                    repository.Update(emp);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            
        }


    }
}

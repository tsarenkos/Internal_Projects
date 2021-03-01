using System.Collections.Generic;
using Factory.Storage.Core.Interfaces;
using Factory.Storage.Core.Entities;
using System;


namespace Factory.EF.DAL.Repositories
{
    public class EmployeeRepository:IRepository<Employee>
    {
        private readonly FactoryContext context;
        public EmployeeRepository(FactoryContext context)
        {
            this.context = context;
        }
        public IEnumerable<Employee> GetAll()
        {
            return context.Employees;
        }
        public Employee GetById(int id)
        {
            if (id <= 0)
            {
                return null;                
            }

            Employee emp = context.Employees.Find(id);
            if (emp == null)
            {
                return null;
            }

            return emp;
        }
        public void Create(Employee emp)
        {
            try
            {
                if (emp == null)
                {
                    throw new ArgumentNullException();
                }
                context.Employees.Add(emp);
            }
            catch (Exception)
            {

                throw;
            }                      
        }
        public void Update(Employee emp)
        {
            try
            {
                if (emp == null)
                {
                    throw new ArgumentNullException();
                }
                
                if (context.Employees.Find(emp.EmployeeId) != null)
                {
                    context.Employees.Update(emp);
                }
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

                Employee emp = context.Employees.Find(id);
                if (emp != null)
                {
                    context.Employees.Remove(emp);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }                   
        }
    }
}

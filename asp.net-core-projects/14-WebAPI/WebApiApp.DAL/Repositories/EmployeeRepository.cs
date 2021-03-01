using System;
using System.Collections.Generic;
using System.Linq;
using WebApiApp.DAL.Entities;
using WebApiApp.DAL.Interfaces;

namespace WebApiApp.DAL.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly FactoryDataBaseContext context;
        public EmployeeRepository()
        {
            this.context = new FactoryDataBaseContext();
        }
        public void Create(Employee item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException();
                }
                
                context.Add(item);
                context.SaveChanges();                
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
                    context.SaveChanges();
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }                        
        }

        public Employee Get(int id)
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

        public IEnumerable<Employee> GetAll()
        {
            return context.Employees;
        }

        public void Update(Employee item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException();
                }
                Employee emp = context.Employees.FirstOrDefault(e => e.EmployeeId == item.EmployeeId);

                if (emp != null)
                {
                    context.Employees.Update(item);
                    context.SaveChanges();
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
                        
        }
    }
}

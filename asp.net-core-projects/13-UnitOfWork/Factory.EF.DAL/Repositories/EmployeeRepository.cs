using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Factory.Storage.Core.Interfaces;
using Factory.Storage.Core.Entities;

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
            if (id != 0)
            {
                return context.Employees.Find(id);
            }
            return null;
        }
        public void Create(Employee emp)
        {
            if (emp != null)
            {
                context.Employees.Add(emp);
            }            
        }
        public void Update(Employee emp)
        {
            if (emp != null)
            {
                context.Entry(emp).State = EntityState.Modified;
            }            
        }
        public void Delete(int id)
        {
            if (id != 0)
            {
                Employee emp = context.Employees.Find(id);
                if (emp != null)
                {
                    context.Employees.Remove(emp);
                }
            }            
        }
    }
}

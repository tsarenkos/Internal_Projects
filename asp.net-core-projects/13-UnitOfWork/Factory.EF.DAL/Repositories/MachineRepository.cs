using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Factory.Storage.Core.Interfaces;
using Factory.Storage.Core.Entities;

namespace Factory.EF.DAL.Repositories
{
    public class MachineRepository:IRepository<Machine>
    {
        private readonly FactoryContext context;
        public MachineRepository(FactoryContext context)
        {
            this.context = context;
        }

        public IEnumerable<Machine> GetAll()
        {
            return context.Machines;
        }

        public Machine GetById(int id)
        {
            if (id != 0)
            {
                return context.Machines.Find(id);
            }
            return null;
        }

        public void Create(Machine machine)
        {
            if (machine != null)
            {
                context.Machines.Add(machine);
            }            
        }
        public void Update(Machine machine)
        {
            if (machine != null)
            {
                context.Entry(machine).State = EntityState.Modified;
            }            
        }

        public void Delete(int id)
        {
            if (id != 0)
            {
                Machine machine = context.Machines.Find(id);
                if (machine != null)
                {
                    context.Machines.Remove(machine);
                }
            }            
        }
    }
}

using System.Collections.Generic;
using System;
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
            if (id <= 0)
            {
                return null;
            }

            Machine machine = context.Machines.Find(id);
            if (machine == null)
            {
                return null;
            }
            return machine;
        }

        public void Create(Machine machine)
        {
            try
            {
                if (machine == null)
                {
                    throw new ArgumentNullException();
                }
                context.Machines.Add(machine);
            }
            catch (Exception exc)
            {
                throw exc;
            }                     
        }
        public void Update(Machine machine)
        {
            try
            {
                if (machine == null)
                {
                    throw new ArgumentNullException();
                }

                if (context.Machines.Find(machine.MachineId) != null)
                {
                    context.Machines.Update(machine);
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
                Machine machine = context.Machines.Find(id);
                if (machine != null)
                {
                    context.Machines.Remove(machine);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}

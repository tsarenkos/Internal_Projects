using Factory.Storage.Core.Entities;
using Factory.Storage.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace Factory.EF.DAL.Repositories
{
    public class DelivererRepository:IRepository<Deliverer>
    {
        private readonly FactoryContext context;
        public DelivererRepository(FactoryContext context)
        {
            this.context = context;
        }
        public IEnumerable<Deliverer> GetAll()
        {
            return context.Deliverers;
        }
        public Deliverer GetById(int id)
        {
            if (id != 0)
            {
                return context.Deliverers.Find(id);
            }
            return null;
        }
        public void Create(Deliverer deliverer)
        {
            if (deliverer != null)
            {
                context.Deliverers.Add(deliverer);
            }            
        }
        public void Update(Deliverer deliverer)
        {
            if (deliverer != null)
            {
                context.Entry(deliverer).State = EntityState.Modified;
            }            
        }
        public void Delete(int id)
        {
            if (id != 0)
            {
                Deliverer deliverer = context.Deliverers.Find(id);
                if (deliverer != null)
                {
                    context.Deliverers.Remove(deliverer);
                }
            }            
        }
    }
}

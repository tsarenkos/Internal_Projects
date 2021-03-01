using Factory.Storage.Core.Entities;
using Factory.Storage.Core.Interfaces;
using System;
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
            if (id <= 0)
            {
                return null;
            }

            Deliverer deliverer = context.Deliverers.Find(id);
            if (deliverer == null)
            {
                return null;
            }
            return deliverer;
        }
        public void Create(Deliverer deliverer)
        {
            try
            {
                if (deliverer == null)
                {
                    throw new ArgumentNullException();
                }
                context.Deliverers.Add(deliverer);
            }
            catch (Exception exc)
            {
                throw exc;
            }                     
        }
        public void Update(Deliverer deliverer)
        {
            try
            {
                if (deliverer == null)
                {
                    throw new ArgumentNullException();
                }
                
                if (context.Deliverers.Find(deliverer.DelivererId) != null)
                {
                    context.Deliverers.Update(deliverer);
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
                Deliverer deliverer = context.Deliverers.Find(id);
                if (deliverer != null)
                {
                    context.Deliverers.Remove(deliverer);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }                     
        }
    }
}

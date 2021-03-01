using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Factory.Storage.Core.Interfaces;
using Factory.Storage.Core.Entities;

namespace Factory.EF.DAL.Repositories
{
    public class RequestRepository:IRepository<Request>
    {
        private readonly FactoryContext context;
        public RequestRepository(FactoryContext context)
        {
            this.context = context;
        }

        public IEnumerable<Request> GetAll()
        {
            return context.Requests;
        }

        public Request GetById(int id)
        {
            if (id != 0)
            {
                return context.Requests.Find(id);
            }
            return null;
        }

        public void Create(Request request)
        {
            if (request != null)
            {
                context.Requests.Add(request);
            }            
        }
        public void Update(Request request)
        {
            if (request != null)
            {
                context.Entry(request).State = EntityState.Modified;
            }            
        }

        public void Delete(int id)
        {
            if (id != 0)
            {
                Request request = context.Requests.Find(id);
                if (request != null)
                {
                    context.Requests.Remove(request);
                }
            }            
        }
    }
}

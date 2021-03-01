using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
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
            if (id <= 0)
            {
                return null;
            }

            Request request = context.Requests.Find(id);
            if (request == null)
            {
                return null;
            }
            return request;
        }

        public void Create(Request request)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException();
                }
                context.Requests.Add(request);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public void Update(Request request)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException();
                }

                if (context.Requests.Find(request.RequestId) != null)
                {
                    context.Requests.Update(request);
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
                Request request = context.Requests.Find(id);
                if (request != null)
                {
                    context.Requests.Remove(request);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }                      
        }
    }
}

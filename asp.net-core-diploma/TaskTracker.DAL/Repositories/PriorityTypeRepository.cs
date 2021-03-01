using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskTracker.DAL.Models;
using TaskTracker.Storage.Core.Entities;
using TaskTracker.Storage.Core.Interfaces;

namespace TaskTracker.DAL.Repositories
{
    public class PriorityTypeRepository : IRepository<PriorityType, int>
    {
        private readonly ApplicationDbContext context;
        public PriorityTypeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<PriorityType> GetAll()
        {
            return context.Priorities;
        }
        public PriorityType GetById(int id)
        {
            if (id != 0)
            {
                return context.Priorities.Find(id);
            }
            return null;
        }
        public void Create(PriorityType item)
        {
            if (item != null)
            {
                context.Priorities.Add(item);
            }
        }
        public void Update(PriorityType item)
        {
            if (item != null)
            {
                context.Entry(item).State = EntityState.Modified;
            }
        }
        public void Delete(PriorityType item)
        {
            if (item != null)
            {
                context.Priorities.Remove(item);
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskTracker.DAL.Models;
using TaskTracker.Storage.Core.Entities;
using TaskTracker.Storage.Core.Interfaces;

namespace TaskTracker.DAL.Repositories
{
    public class PeriodTypeRepository : IRepository<PeriodType, int>
    {
        private readonly ApplicationDbContext context;
        public PeriodTypeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<PeriodType> GetAll()
        {
            return context.PeriodTypes;
        }
        public PeriodType GetById(int id)
        {
            if (id != 0)
            {
                return context.PeriodTypes.Find(id);
            }
            return null;
        }
        public void Create(PeriodType item)
        {
            if (item != null)
            {
                context.PeriodTypes.Add(item);
            }
        }
        public void Update(PeriodType item)
        {
            if (item != null)
            {
                context.Entry(item).State = EntityState.Modified;
            }
        }
        public void Delete(PeriodType item)
        {
            if (item != null)
            {
                context.PeriodTypes.Remove(item);
            }            
        }
    }
}

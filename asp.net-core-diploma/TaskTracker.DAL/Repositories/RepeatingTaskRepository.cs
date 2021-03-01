using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskTracker.DAL.Models;
using TaskTracker.Storage.Core.Entities;
using TaskTracker.Storage.Core.Interfaces;

namespace TaskTracker.DAL.Repositories
{
    public class RepeatingTaskRepository: IRepository<RepeatingTask, int>
    {
        private readonly ApplicationDbContext context;
        public RepeatingTaskRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<RepeatingTask> GetAll()
        {
            return context.RepeatingTasks.Include(rt => rt.PeriodType);
        }
        public RepeatingTask GetById(int id)
        {
            if (id != 0)
            {
                return context.RepeatingTasks.Find(id);
            }
            return null;
        }
        public void Create(RepeatingTask item)
        {
            if (item != null)
            {
                context.RepeatingTasks.Add(item);
            }
        }
        public void Update(RepeatingTask item)
        {
            if (item != null)
            {
                context.Entry(item).State = EntityState.Modified;
            }
        }
        public void Delete(RepeatingTask item)
        {
            if (item != null)
            {
                context.RepeatingTasks.Remove(item);
            }            
        }
    }
}

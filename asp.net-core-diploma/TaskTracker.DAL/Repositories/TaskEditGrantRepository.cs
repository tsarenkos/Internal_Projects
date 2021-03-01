using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskTracker.DAL.Models;
using TaskTracker.Storage.Core.Entities;
using TaskTracker.Storage.Core.Interfaces;

namespace TaskTracker.DAL.Repositories
{
    public class TaskEditGrantRepository : IRepository<TaskEditGrants, int>
    {
        private readonly ApplicationDbContext context;
        public TaskEditGrantRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<TaskEditGrants> GetAll()
        {
            return context.TaskEditGrants;
        }
        public TaskEditGrants GetById(int id)
        {
            if (id != 0)
            {
                return context.TaskEditGrants.Find(id);
            }
            return null;
        }
        public void Create(TaskEditGrants item)
        {
            if (item != null)
            {
                context.TaskEditGrants.Add(item);
            }
        }
        public void Update(TaskEditGrants item)
        {
            if (item != null)
            {
                context.Entry(item).State = EntityState.Modified;
            }
        }
        public void Delete(TaskEditGrants item)
        {
            if (item != null)
            {
                context.TaskEditGrants.Remove(item);
            }            
        }
    }
}

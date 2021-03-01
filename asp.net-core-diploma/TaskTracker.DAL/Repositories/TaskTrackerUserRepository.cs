using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskTracker.DAL.Models;
using TaskTracker.Storage.Core.Entities;
using TaskTracker.Storage.Core.Interfaces;

namespace TaskTracker.DAL.Repositories
{
    public class TaskTrackerUserRepository : IRepository<TaskTrackerUser, string>
    {
        private readonly ApplicationDbContext context;
        public TaskTrackerUserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<TaskTrackerUser> GetAll()
        {
            return context.TaskTrackerUser;
        }
        public TaskTrackerUser GetById(string id)
        {
            if (id != null)
            {
                return context.TaskTrackerUser.Find(id);
            }
            return null;
        }
        public void Create(TaskTrackerUser item)
        {
            if (item != null)
            {
                context.TaskTrackerUser.Add(item);
            }
        }
        public void Update(TaskTrackerUser item)
        {
            if (item != null)
            {
                context.Entry(item).State = EntityState.Modified;
            }
        }
        public void Delete(TaskTrackerUser item)
        {
            if (item != null)
            {
                context.TaskTrackerUser.Remove(item);
            }            
        }
    }
}

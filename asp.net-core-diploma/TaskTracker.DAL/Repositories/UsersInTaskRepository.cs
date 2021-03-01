using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskTracker.DAL.Models;
using TaskTracker.Storage.Core.Entities;
using TaskTracker.Storage.Core.Interfaces;

namespace TaskTracker.DAL.Repositories
{
    public class UsersInTaskRepository : IRepository<UsersInTask, string>
    {
        private readonly ApplicationDbContext context;
        public UsersInTaskRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<UsersInTask> GetAll()
        {
            return context.UsersInTasks;
        }
        public UsersInTask GetById(string id)
        {
            if (id != null)
            {
                return context.UsersInTasks.Find(id);
            }
            return null;
        }
        public void Create(UsersInTask item)
        {
            if (item != null)
            {
                context.UsersInTasks.Add(item);
            }
        }
        public void Update(UsersInTask item)
        {
            if (item != null)
            {
                context.Entry(item).State = EntityState.Modified;
            }
        }
        public void Delete(UsersInTask item)
        {
            if (item != null)
            {
                context.UsersInTasks.Remove(item);
            }            
        }
    }
}

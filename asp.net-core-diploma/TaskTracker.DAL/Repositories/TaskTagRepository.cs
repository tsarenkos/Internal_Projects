using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskTracker.DAL.Models;
using TaskTracker.Storage.Core.Entities;
using TaskTracker.Storage.Core.Interfaces;

namespace TaskTracker.DAL.Repositories
{
    public class TaskTagRepository : IRepository<TaskTag, int>
    {
        private readonly ApplicationDbContext context;
        public TaskTagRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<TaskTag> GetAll()
        {
            return context.TaskTags;
        }
        public TaskTag GetById(int id)
        {
            if (id != 0)
            {
                return context.TaskTags.Find(id);
            }
            return null;
        }
        public void Create(TaskTag item)
        {
            if (item != null)
            {
                context.TaskTags.Add(item);
            }
        }
        public void Update(TaskTag item)
        {
            if (item != null)
            {
                context.Entry(item).State = EntityState.Modified;
            }
        }
        public void Delete(TaskTag item)
        {
            if (item != null)
            {
                context.TaskTags.Remove(item);
            }            
        }
    }
}

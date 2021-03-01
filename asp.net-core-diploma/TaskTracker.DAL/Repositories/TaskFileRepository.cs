using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskTracker.DAL.Models;
using TaskTracker.Storage.Core.Entities;
using TaskTracker.Storage.Core.Interfaces;

namespace TaskTracker.DAL.Repositories
{
    public class TaskFileRepository : IRepository<TaskFile, int>
    {
        private readonly ApplicationDbContext context;
        public TaskFileRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<TaskFile> GetAll()
        {
            return context.TasksFiles;
        }
        public TaskFile GetById(int id)
        {
            if (id != 0)
            {
                return context.TasksFiles.Find(id);
            }
            return null;
        }
        public void Create(TaskFile item)
        {
            if (item != null)
            {
                context.TasksFiles.Add(item);
            }
        }
        public void Update(TaskFile item)
        {
            if (item != null)
            {
                context.Entry(item).State = EntityState.Modified;
            }
        }
        public void Delete(TaskFile item)
        {
            if (item != null)
            {
                context.TasksFiles.Remove(item);
            }           
        }
    }
}

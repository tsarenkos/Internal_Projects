using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskTracker.DAL.Models;
using TaskTracker.Storage.Core.Entities;
using TaskTracker.Storage.Core.Interfaces;

namespace TaskTracker.DAL.Repositories
{
    public class TaskCategoryRepository : IRepository<TaskСategory, int>
    {
        private readonly ApplicationDbContext context;
        public TaskCategoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<TaskСategory> GetAll()
        {
            return context.TaskСategories;
        }
        public TaskСategory GetById(int id)
        {
            if (id != 0)
            {
                return context.TaskСategories.Find(id);
            }
            return null;
        }
        public void Create(TaskСategory item)
        {
            if (item != null)
            {
                context.TaskСategories.Add(item);
            }
        }
        public void Update(TaskСategory item)
        {
            if (item != null)
            {
                context.Entry(item).State = EntityState.Modified;
            }
        }
        public void Delete(TaskСategory item)
        {
            if (item != null)
            {
                context.TaskСategories.Remove(item);
            }            
        }
    }
}

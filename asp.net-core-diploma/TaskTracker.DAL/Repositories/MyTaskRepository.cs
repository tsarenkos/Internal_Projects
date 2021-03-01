using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskTracker.DAL.Models;
using TaskTracker.Storage.Core.Entities;
using TaskTracker.Storage.Core.Interfaces;

namespace TaskTracker.DAL.Repositories
{
    public class MyTaskRepository:IRepository<MyTask, int>
    {
        private readonly ApplicationDbContext context;
        public MyTaskRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<MyTask> GetAll()
        {
            return context.MyTasks;
        }
        public MyTask GetById(int id)
        {
            if (id != 0)
            {
                return context.MyTasks.Find(id);
            }
            return null;
        }
        public void Create(MyTask task)
        {
            if (task != null)
            {
                context.MyTasks.Add(task);
            }
        }
        public void Update(MyTask task)
        {
            if (task != null)
            {
                context.Entry(task).State = EntityState.Modified;
            }
        }
        public void Delete(MyTask task)
        {
            if (task != null)
            {
                context.MyTasks.Remove(task);
            }
        }
    }
}

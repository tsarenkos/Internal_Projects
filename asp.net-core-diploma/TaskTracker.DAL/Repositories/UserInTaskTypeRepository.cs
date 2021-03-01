using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskTracker.DAL.Models;
using TaskTracker.Storage.Core.Entities;
using TaskTracker.Storage.Core.Interfaces;

namespace TaskTracker.DAL.Repositories
{
    public class UserInTaskTypeRepository : IRepository<UserInTaskType, int>
    {
        private readonly ApplicationDbContext context;
        public UserInTaskTypeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<UserInTaskType> GetAll()
        {
            return context.UserInTaskTypes;
        }
        public UserInTaskType GetById(int id)
        {
            if (id != 0)
            {
                return context.UserInTaskTypes.Find(id);
            }
            return null;
        }
        public void Create(UserInTaskType item)
        {
            if (item != null)
            {
                context.UserInTaskTypes.Add(item);
            }
        }
        public void Update(UserInTaskType item)
        {
            if (item != null)
            {
                context.Entry(item).State = EntityState.Modified;
            }
        }
        public void Delete(UserInTaskType item)
        {
            if (item != null)
            {
                context.UserInTaskTypes.Remove(item);
            }            
        }
    }
}

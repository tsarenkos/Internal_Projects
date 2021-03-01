using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskTracker.DAL.Models;
using TaskTracker.Storage.Core.Entities;
using TaskTracker.Storage.Core.Interfaces;

namespace TaskTracker.DAL.Repositories
{
    public class NotificationUserRepository:IRepository<NotificationUser, int>
    {
        private readonly ApplicationDbContext context;
        public NotificationUserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<NotificationUser> GetAll()
        {
            return context.NotificationUsers;
        }
        public NotificationUser GetById(int id)
        {
            if (id != 0)
            {
                return context.NotificationUsers.Find(id);
            }
            return null;
        }
        public void Create(NotificationUser user)
        {
            if (user != null)
            {
                context.NotificationUsers.Add(user);
            }
        }
        public void Update(NotificationUser user)
        {
            if (user != null)
            {
                context.Entry(user).State = EntityState.Modified;
            }
        }
        public void Delete(NotificationUser user)
        {
            if (user != null)
            {
                context.NotificationUsers.Remove(user);
            }
        }
    }
}

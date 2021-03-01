using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskTracker.DAL.Models;
using TaskTracker.Storage.Core.Entities;
using TaskTracker.Storage.Core.Interfaces;

namespace TaskTracker.DAL.Repositories
{
    public class NotificationRepository:IRepository<Notification, int>
    {
        private readonly ApplicationDbContext context;
        public NotificationRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Notification> GetAll()
        {
            return context.Notifications;
        }
        public Notification GetById(int id)
        {
            if (id != 0)
            {
                return context.Notifications.Find(id);
            }
            return null;
        }
        public void Create(Notification message)
        {
            if (message != null)
            {
                context.Notifications.Add(message);
            }
        }
        public void Update(Notification message)
        {
            if (message != null)
            {
                context.Entry(message).State = EntityState.Modified;
            }
        }
        public void Delete(Notification message)
        {
            if (message != null)
            {
                context.Notifications.Remove(message);
            }
        }
    }
}

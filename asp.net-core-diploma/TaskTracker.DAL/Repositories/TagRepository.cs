using TaskTracker.DAL.Models;
using TaskTracker.Storage.Core.Entities;
using TaskTracker.Storage.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace TaskTracker.DAL.Repositories
{
    public class TagRepository: IRepository<Tag, int>
    {
        private readonly ApplicationDbContext context;
        public TagRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Tag> GetAll()
        {
            return context.Tags;
        }
        public Tag GetById(int id)
        {
            if (id != 0)
            {
                return context.Tags.Find(id);
            }
            return null;
        }
        public void Create(Tag item)
        {
            if (item != null)
            {
                context.Tags.Add(item);
            }
        }
        public void Update(Tag item)
        {
            if (item != null)
            {
                context.Entry(item).State = EntityState.Modified;
            }
        }
        public void Delete(Tag item)
        {
            if (item != null)
            {
                context.Tags.Remove(item);
            }            
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskTracker.DAL.Models;
using TaskTracker.Storage.Core.Entities;
using TaskTracker.Storage.Core.Interfaces;

namespace TaskTracker.DAL.Repositories
{
    public class MyFileRepository:IRepository<MyFile, int>
    {
        private readonly ApplicationDbContext context;
        public MyFileRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<MyFile> GetAll()
        {
            return context.Files;
        }
        public MyFile GetById(int id)
        {
            if (id != 0)
            {
                return context.Files.Find(id);
            }
            return null;
        }
        public void Create(MyFile file)
        {
            if (file != null)
            {
                context.Files.Add(file);
            }            
        }
        public void Update(MyFile file)
        {
            if (file != null)
            {
                context.Entry(file).State = EntityState.Modified;
            }            
        }
        public void Delete(MyFile file)
        {
            if (file != null)
            {
                context.Files.Remove(file);
            }
        }
    }
}

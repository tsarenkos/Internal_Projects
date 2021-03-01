using System;
using System.Collections.Generic;
using System.Linq;
using TaskTracker.BL.Interfaces;
using TaskTracker.Models;
using TaskTracker.Storage.Core.Entities;
using TaskTracker.Storage.Core.Interfaces;

namespace TaskTracker.BL.Services
{
    public class TaskCategoryService: ICategoryServiceBL
    {
        private readonly IUnitOfWork context;
        private readonly ICheckUserService checkUserService;
        public TaskCategoryService(IUnitOfWork context, ICheckUserService checkUserService)
        {
            this.context = context;
            this.checkUserService = checkUserService;
        }
        public IEnumerable<TaskCategoryModelBL> GetAll()
        {
            List<TaskCategoryModelBL> taskCategoryList = new List<TaskCategoryModelBL>();

            try
            {
                IEnumerable<TaskСategory> taskCategory = context.TaskСategories.GetAll();

                foreach (var category in taskCategory)
                {
                    taskCategoryList.Add(new TaskCategoryModelBL()
                    {
                        Id = category.Id,
                        Name = category.Name
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }

            return taskCategoryList;
        }
        public TaskCategoryModelBL GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentNullException();
                }

                return GetAll().FirstOrDefault(p => p.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Add(string categoryName)
        {
            try
            {
                if (categoryName == null || categoryName=="")
                {
                    throw new ArgumentNullException();
                }

                TaskСategory taskCategory = new TaskСategory() { 
                    Name=categoryName 
                };
                context.TaskСategories.Create(taskCategory);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Edit(int categoryId, string categoryName)
        {
            try
            {
                if (categoryId <= 0 || categoryName == null || categoryName=="")
                {
                    throw new ArgumentNullException();
                }

                TaskСategory taskCategory = new TaskСategory()
                {
                    Id = categoryId,
                    Name = categoryName
                };

                context.TaskСategories.Update(taskCategory);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentNullException();
                }

                TaskСategory taskСategory = context.TaskСategories.GetAll().FirstOrDefault(c => c.Id == id);

                if (taskСategory == null)
                {
                    throw new ArgumentNullException();
                }

                context.TaskСategories.Delete(taskСategory);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UpdateCategoryForTask(int taskId, string UserId, bool IsAdmin, TaskCategoryModelBL taskCategory)
        {
            try
            {
                if (taskId <= 0 || taskCategory == null)
                {
                    throw new ArgumentNullException();
                }
                if (!checkUserService.AccessGrantedForUser(taskId))
                {
                    throw new UnauthorizedAccessException();
                }

                MyTask myTask = context.MyTasks.GetAll().FirstOrDefault(t => t.Id == taskId);

                if (myTask == null)
                {
                    throw new ArgumentNullException();
                }

                myTask.TaskСategoryId = taskCategory.Id;

                context.MyTasks.Update(myTask);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

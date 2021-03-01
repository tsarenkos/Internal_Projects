using System;
using System.Collections.Generic;
using System.Linq;
using TaskTracker.BL.Interfaces;
using TaskTracker.Models;
using TaskTracker.Storage.Core.Entities;
using TaskTracker.Storage.Core.Interfaces;

namespace TaskTracker.BL.Services
{
    public class TaskTagService : ITaskTagServiceBL
    {
        private readonly IUnitOfWork context;
        private readonly ICheckUserService checkUserService;


        public TaskTagService(IUnitOfWork context, ICheckUserService checkUserService)
        {
            this.context = context;
            this.checkUserService = checkUserService;
        }

        public IEnumerable<TaskTagModelBL> GetAll()
        {
            List<TaskTagModelBL> taskTagList = new List<TaskTagModelBL>();

            try
            {
                IEnumerable<Tag> tagForTasks = context.Tags.GetAll();

                foreach (var tag in tagForTasks)
                {
                    taskTagList.Add(new TaskTagModelBL()
                    {
                        Id = tag.Id,
                        Name = tag.Name,
                        TaskId = context.TaskTags.GetAll().FirstOrDefault(t => t.TagId == tag.Id).MyTaskId
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }

            return taskTagList;
        }
        public TaskTagModelBL GetTagById(int id)
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
        public IEnumerable<TaskTagModelBL> GetTagByTaskId(int taskId)
        {
            try
            {
                if (taskId <= 0)
                {
                    throw new ArgumentNullException();
                }
                if (!checkUserService.AccessGrantedForUser(taskId))
                {
                    throw new UnauthorizedAccessException();
                }

                IEnumerable<TaskTag> tagForTasks = context.TaskTags.GetAll().Where(t=>t.MyTaskId==taskId);
                List<TaskTagModelBL> taskTagList = new List<TaskTagModelBL>();

                foreach (var taskTag in tagForTasks)
                {
                    taskTagList.Add(new TaskTagModelBL()
                    {
                        Id = taskTag.TagId,
                        Name = GetAll().FirstOrDefault(t=>t.Id==taskTag.TagId).Name,
                        TaskId = taskTag.MyTaskId
                    });
                }

                return taskTagList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Add(int taskId, string tagName)
        {
            try
            {
                if (taskId<=0 || tagName == null || tagName == "")
                {
                    throw new ArgumentNullException();
                }
                if (!checkUserService.AccessGrantedForUser(taskId))
                {
                    throw new UnauthorizedAccessException();
                }

                MyTask myTask = context.MyTasks.GetAll().FirstOrDefault(f => f.Id == taskId);

                if (myTask == null)
                {
                    throw new ArgumentNullException();
                }

                Tag tag = new Tag()
                {
                    Name = tagName
                };

                context.Tags.Create(tag);
                context.SaveChanges();

                if (tag.Id == 0)
                {
                    Delete(tag.Id);
                    throw new ArgumentNullException();
                }

                context.TaskTags.Create(new TaskTag() { TagId = tag.Id, MyTaskId = taskId });
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Edit(int tagId, string tagName)
        {
            try
            {
                if (tagId <= 0 || tagName == null || tagName=="")
                {
                    throw new ArgumentNullException();
                }

                Tag tagForTask = context.Tags.GetAll().FirstOrDefault(t => t.Id == tagId);
                TaskTag taskTag = context.TaskTags.GetAll().FirstOrDefault(t => t.TagId == tagId);

                if (tagForTask == null || taskTag==null)
                {
                    throw new ArgumentNullException();
                }
                if (!checkUserService.AccessGrantedForUser(taskTag.MyTaskId))
                {
                    throw new UnauthorizedAccessException();
                }

                tagForTask.Name = tagName;

                context.Tags.Update(tagForTask);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Delete(int tagId)
        {
            try
            {
                if (tagId <= 0)
                {
                    throw new ArgumentNullException();
                }

                Tag tag = context.Tags.GetAll().FirstOrDefault(f => f.Id == tagId);
                TaskTag taskTag = context.TaskTags.GetAll().FirstOrDefault(t => t.TagId == tagId);

                if (tag == null || taskTag==null)
                {
                    throw new ArgumentNullException();
                }
                if (!checkUserService.AccessGrantedForUser(taskTag.MyTaskId))
                {
                    throw new UnauthorizedAccessException();
                }

                context.Tags.Delete(tag);
                context.TaskTags.Delete(taskTag);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

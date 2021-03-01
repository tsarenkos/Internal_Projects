using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskTracker.BL.Interfaces;
using TaskTracker.Models;
using TaskTracker.Shared.Extensions;
using TaskTracker.Storage.Core.Entities;
using TaskTracker.Storage.Core.Interfaces;

namespace TaskTracker.BL.Services
{
    public class SubTaskService : ISubTaskService
    {
        private readonly IUnitOfWork context;
        private readonly IHttpContextAccessor _httpcontext;
 
        public SubTaskService(IHttpContextAccessor httpcontext, IUnitOfWork context, IFileService fileService)
        {
            _httpcontext = httpcontext;
            this.context = context;
        }

        public IEnumerable<TaskModelBL> GetNonSubTask(int TaskId)
        {
            IEnumerable<MyTask> list = null;

            if (HttpContextExtensions.IsAdmin(_httpcontext))
                list = context.MyTasks.GetAll();
            else
            {
                var idlist = context.UsersInTasks.GetAll().Where(u => u.UserId == HttpContextExtensions.GetUserId(_httpcontext)).Select(u => u.MyTaskId);
                var subtasks = context.MyTasks.GetAll().Where(st=>st.ParentTaskId==TaskId).Select(u => u.Id);
                list = context.MyTasks.GetAll().Where(t => idlist.Contains(t.Id) && t.Id!=TaskId && !subtasks.Any(st=>st==t.Id));
            }

            return list.Select(task => new TaskModelBL()
            {
                Id = task.Id,
                Name = task.Name,
                StartDate = task.StartDate,
                TargetDate = task.TargetDate,
                EndDate = task.EndDate,
                Details = task.Details,
                IsRepeating = task.IsRepeating,
                TaskСategoryId = task.TaskСategoryId,
                TaskPriorityId = task.TaskPriorityId,
                ParentTaskId = task.ParentTaskId,
                files = null
            });
        }
    }
}

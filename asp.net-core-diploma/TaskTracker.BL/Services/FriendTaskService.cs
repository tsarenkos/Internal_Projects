using System;
using System.Collections.Generic;
using TaskTracker.BL.Interfaces;
using TaskTracker.Models;
using TaskTracker.DAL.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Http;
using TaskTracker.Shared.Extensions;
using TaskTracker.Shared.Interfaces;
using System.Threading.Tasks;
using TaskTracker.Storage.Core.Entities;

namespace TaskTracker.BL.Services
{
    // список задач с проверкой принадлежности пользователю
    public class FriendTaskService: IFriendTaskService
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor _httpcontext;
        private readonly IFileService _fileService;
        private readonly IMyTokenService _tokenService;
        private readonly IIdentityClient _client;

        public FriendTaskService(IHttpContextAccessor httpcontext, ApplicationDbContext context, IFileService fileService, IMyTokenService tokenService, IIdentityClient client)
        {
            _httpcontext = httpcontext;
            this.context = context;
            _fileService = fileService;
            _tokenService = tokenService;
            _client = client;
        }

        public async Task<IEnumerable<TaskModelBL>> GetAll(string FriendId)
        {
            var token = await _tokenService.GetIdentityToken(new LoginViewModel()
            {
                UserName = "Dwain",
                Password = "Dwain"
            });
            if(!token.Success)
            {
                throw new Exception(token.Error);
            }

            var friends = await _client.Get<IEnumerable<UserFriendBL>>("api/friends");

            IEnumerable<MyTask> list = null;

            if (HttpContextExtensions.IsAdmin(_httpcontext))
                list = context.MyTasks;
            else
            {
                var idlist = context.UsersInTasks.Where(u => u.UserId == FriendId).Select(u => u.MyTaskId);
                list = context.MyTasks.Where(t => idlist.Contains(t.Id));
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

        private bool CheckAccess(int id, out MyTask task)
        {
            task = null;
            if (id <= 0) return false;

            task = context.MyTasks.Find(id);

            if (task == null) return false;

            return HttpContextExtensions.IsAdmin(_httpcontext) || context.UsersInTasks.Any(u => u.MyTaskId == id && u.UserId == HttpContextExtensions.GetUserId(_httpcontext));
        }

        public TaskModelBL GetById(int id)
        {
            if (!CheckAccess(id, out MyTask task)) return null;

            return new TaskModelBL
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
                files = _fileService.GetAll(id).ToList()
            };
        }

        public bool Update(int id, TaskModelBL model)
        {
            if (model == null) return false;

            if (!CheckAccess(id, out MyTask task)) return false;

            task.Name = model.Name;
            task.TargetDate = model.TargetDate;
            task.Details = model.Details;
            task.IsRepeating = model.IsRepeating;
            task.TaskСategoryId = model.TaskСategoryId;
            task.TaskPriorityId = model.TaskPriorityId;
            task.ParentTaskId = model.ParentTaskId;
            context.MyTasks.Update(task);
            context.SaveChanges();

            var list = context.TasksFiles.Where(tf => tf.TaskId == id).Select(tf=>tf.FileId).ToList();

            foreach (int FileId in list)
            {
                if (!model.files.Any(fl => fl.Id == FileId))
                    _fileService.Delete(FileId);
            }

            foreach (FileModelBL fl in model.files.Where(fl => !string.IsNullOrWhiteSpace(fl.FileName)))
            {
                _fileService.Create(id, fl);
            }

            return true;
        }
    }
}

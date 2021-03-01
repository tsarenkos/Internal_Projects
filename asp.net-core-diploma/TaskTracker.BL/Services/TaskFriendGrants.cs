using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.BL.Interfaces;
using TaskTracker.DAL.Models;
using TaskTracker.Models;
using TaskTracker.Shared.Extensions;
using TaskTracker.Storage.Core.Entities;
using TaskTracker.Storage.Core.Interfaces;
using TaskTracker.Shared.Interfaces;
using System;

namespace TaskTracker.BL.Services
{
    public class TaskFriendGrants : ITaskFriendGrants
    {
        private readonly IUnitOfWork context;
        private readonly IHttpContextAccessor _httpcontext;
        private readonly IMailService _mailService;
        private readonly IIdentityClient _client;
        private readonly IMyTokenService _tokenService;

        public TaskFriendGrants(IHttpContextAccessor httpcontext, IUnitOfWork context, IMailService mailService, IIdentityClient client, IMyTokenService tokenService)
        {
            _httpcontext = httpcontext;
            this.context = context;
            _mailService = mailService;
            _tokenService = tokenService;
            _client = client;
        }

        public IEnumerable<TaskEditGrantsBL> GetAll()
        {
            string UserId = HttpContextExtensions.GetUserId(_httpcontext);
            var taskIds = context.UsersInTasks.GetAll().Where(ut => ut.UserId == UserId && ut.UserInTaskTypeCode == 1).Select(ut => ut.MyTaskId);

            var list = (from MyTask in context.MyTasks.GetAll()
                       join TaskEditGrant in context.TaskEditGrants.GetAll() on MyTask.Id equals TaskEditGrant.TaskId
                       select new { task = MyTask, taskgrant = TaskEditGrant }).Where(itm => taskIds.Any(t=>t==itm.task.Id) && itm.taskgrant.IsGranted == false);

            return list.Select(t=>new TaskEditGrantsBL()
            {
                TaskId = t.task.Id,
                Task = new TaskModelBL()
                {
                    Id = t.task.Id,
                    IsRepeating = t.task.IsRepeating,
                    Details = t.task.Details,
                    EndDate = t.task.EndDate,
                    Name = t.task.Name,
                    ParentTaskId = t.task.ParentTaskId,
                    TaskPriorityId = t.task.TaskPriorityId,
                    TargetDate = t.task.TargetDate,
                    TaskСategoryId = t.task.TaskСategoryId,
                    StartDate = t.task.StartDate
                },
                date = t.taskgrant.date,
                FriendId = t.taskgrant.FriendId,
                IsGranted = false
            });
        }

        private APIResult CheckTask(TaskEditGrantsBL taskEditGrantRequest, bool oncreate, out TaskEditGrants taskGrant)
        {
            taskGrant = null;
            int TaskId = taskEditGrantRequest.TaskId;
            MyTask task = context.MyTasks.GetById(TaskId);
            if (task == null)
                return new APIResult() { Success = false, Error = "Неправильный TaskId" };

            string FriendId = taskEditGrantRequest.FriendId;

            if(!context.UsersInTasks.GetAll().Any(ut => ut.UserId == FriendId && ut.MyTaskId == TaskId && ut.UserInTaskTypeCode == 2))
                return new APIResult() { Success = false, Error = "Неправильный FriendId" };

            taskGrant = context.TaskEditGrants.GetAll().FirstOrDefault(teg => teg.FriendId == FriendId && teg.TaskId == TaskId);
            if (taskGrant == null &&!oncreate)
                return new APIResult() { Success = false, Error = "Такого запроса не существует" };

            if (taskGrant != null && oncreate)
                return new APIResult() { Success = false, Error = "Такой запрос уже существует" };

            return new APIResult() { Success = true };
        }

        public async Task<APIResult> Create(TaskEditGrantsBL taskEditGrantRequest)
        {
            APIResult res = CheckTask(taskEditGrantRequest, true, out TaskEditGrants taskGrant);

            if (!res.Success)
                return res;

            var model = new TaskEditGrants()
            {
                TaskId = taskEditGrantRequest.TaskId,
                FriendId = taskEditGrantRequest.FriendId,
                date = taskEditGrantRequest.date,
                IsGranted = false
            };
                context.TaskEditGrants.Create(model);
                await context.SaveChangesAsync();

            var token = await _tokenService.GetIdentityToken(new LoginViewModel()
            {
                UserName = "Dwain",
                Password = "Dwain"
            });
            if (!token.Success)
            {
                throw new Exception(token.Error);
            }


            var UserId = context.UsersInTasks.GetAll().Where(t => t.MyTaskId == model.TaskId && t.UserInTaskTypeCode == 1).FirstOrDefault().UserId;

            var friend = (await _client.GetWithToken<IEnumerable<UserFriendBL>>($"api/friendsforuser/{taskEditGrantRequest.FriendId}", token.token)).FirstOrDefault(u => u.FriendId == UserId);

            if (friend != null)
            {
                await _mailService.SendAsync(new MailModelBL()
                {
                    To = friend.Friend.Email,
                    Subject = "Запрос на редактирование задачи",
                    Body = "Вам пришёл запрос на редактирование задачи <br> Чтобы принять или отклонить запрос перейдите по ссылке: <br> <a href=\"https://localhost:44347/friends\">Перейти...</a>"
                });
            }

            return res;
        }

        public async Task<APIResult> Deny(TaskEditGrantsBL taskEditGrantRequest)
        {
            APIResult res = CheckTask(taskEditGrantRequest, false, out TaskEditGrants taskGrant);
            if (!res.Success)
                return res;

            context.TaskEditGrants.Delete(taskGrant);
            await context.SaveChangesAsync();

            var token = await _tokenService.GetIdentityToken(new LoginViewModel()
            {
                UserName = "Dwain",
                Password = "Dwain"
            });
            if (!token.Success)
            {
                throw new System.Exception(token.Error);
            }

            var UserId = context.UsersInTasks.GetAll().Where(t => t.MyTaskId == taskGrant.TaskId && t.UserInTaskTypeCode == 1).FirstOrDefault().UserId;

            var friend = (await _client.GetWithToken<IEnumerable<UserFriendBL>>($"api/friendsforuser/{taskEditGrantRequest.FriendId}", token.token)).FirstOrDefault(u => u.FriendId == UserId);

            if (friend != null)
            {
                await _mailService.SendAsync(new MailModelBL()
                {
                    To = friend.Friend.Email,
                    Subject = "Запрос-назначение исполнителем на задачу (ОТКЛОНЕНО)",
                    Body = $"Ваш запрос-назначение исполнителем на задачу был отклонён <br> <a href=\"https://localhost:44347/MyTask/\">Перейти к списку задач...</a>"
                });
            }

            return res;
        }

        public async Task<APIResult> Grant(TaskEditGrantsBL taskEditGrantRequest)
        {
            APIResult res = CheckTask(taskEditGrantRequest, false, out TaskEditGrants taskGrant);
            if (!res.Success)
                return res;

            taskGrant.date = taskEditGrantRequest.date;
            taskGrant.IsGranted = true;
            context.TaskEditGrants.Update(taskGrant);
            await context.SaveChangesAsync();

            var token = await _tokenService.GetIdentityToken(new LoginViewModel()
            {
                UserName = "Dwain",
                Password = "Dwain"
            });
            if (!token.Success)
            {
                throw new System.Exception(token.Error);
            }
            var UserId = context.UsersInTasks.GetAll().Where(t => t.MyTaskId == taskGrant.TaskId && t.UserInTaskTypeCode == 1).FirstOrDefault().UserId;

            var friend = (await _client.GetWithToken<IEnumerable<UserFriendBL>>($"api/friendsforuser/{taskEditGrantRequest.FriendId}", token.token)).FirstOrDefault(u => u.FriendId == UserId);

            if (friend != null)
            {
                await _mailService.SendAsync(new MailModelBL()
                {
                    To = friend.Friend.Email,
                    Subject = "Запрос-назначение исполнителем на задачу (ОДОБРЕНО)",
                    Body = $"Ваш запрос-назначение исполнителем на задачу был одобрен <br> Подробности по ссылке: <br> <a href=\"https://localhost:44347/MyTask/Details/{taskEditGrantRequest.TaskId}\">Перейти к задаче...</a>"
                });
            }

            return res;
        }
    }
}

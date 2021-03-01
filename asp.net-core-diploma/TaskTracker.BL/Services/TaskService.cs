using System;
using System.Collections.Generic;
using TaskTracker.BL.Interfaces;
using TaskTracker.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Http;
using TaskTracker.Shared.Extensions;
using TaskTracker.Storage.Core.Entities;
using TaskTracker.Storage.Core.Interfaces;
using TaskTracker.Shared.Interfaces;

namespace TaskTracker.BL.Services
{
    // список задач с проверкой принадлежности пользователю
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork context;
        private readonly IHttpContextAccessor _httpcontext;
        private readonly IFileService _fileService;
        private readonly IMailService _mailService;
        private readonly IIdentityClient _client;
        private readonly IMyTokenService _tokenService;

        public TaskService(IHttpContextAccessor httpcontext, IUnitOfWork context, IFileService fileService, IMailService mailService, IIdentityClient client, IMyTokenService tokenService)
        {
            _httpcontext = httpcontext;
            this.context = context;
            _fileService = fileService;
            _mailService = mailService;
            _tokenService = tokenService;
            _client = client;
        }
        public IEnumerable<TaskModelBL> GetAll()
        {
            if (HttpContextExtensions.IsAdmin(_httpcontext))
                return context.MyTasks.GetAll().Select(task => new TaskModelBL()
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
                    files = null,
                    IsFriendTask = false
                });
            else
            {
                var UserId = HttpContextExtensions.GetUserId(_httpcontext);
                var list = (from MyTask in context.MyTasks.GetAll()
                            join UserInTask in context.UsersInTasks.GetAll() on MyTask.Id equals UserInTask.MyTaskId
                            select new { task = MyTask, usersintask = UserInTask })
                         .Where(x => x.usersintask.UserId == UserId).ToList();

                return list.Select(item => new TaskModelBL()
                {
                    Id = item.task.Id,
                    Name = item.task.Name,
                    StartDate = item.task.StartDate,
                    TargetDate = item.task.TargetDate,
                    EndDate = item.task.EndDate,
                    Details = item.task.Details,
                    IsRepeating = item.task.IsRepeating,
                    TaskСategoryId = item.task.TaskСategoryId,
                    TaskPriorityId = item.task.TaskPriorityId,
                    ParentTaskId = item.task.ParentTaskId,
                    files = null,
                    IsFriendTask = (item.usersintask.UserInTaskTypeCode == 2)
                });
            }
        }

        private bool CheckAccess(int id, out MyTask task)
        {
            task = null;
            if (id <= 0) return false;

            task = context.MyTasks.GetById(id);

            if (task == null) return false;

            return HttpContextExtensions.IsAdmin(_httpcontext) || context.UsersInTasks.GetAll().Any(u => u.MyTaskId == id && u.UserId == HttpContextExtensions.GetUserId(_httpcontext));
        }

        public TaskModelBL GetById(int id)
        {
            bool editGrant = false;

            if (!CheckAccess(id, out MyTask task)) return null;

            string userId = HttpContextExtensions.GetUserId(_httpcontext);

            var grantModel = context.TaskEditGrants.GetAll().FirstOrDefault(gr => gr.TaskId == task.Id && gr.FriendId == userId);

            var isFriendTask = (context.UsersInTasks.GetAll().Where(ut => ut.MyTaskId == task.Id && ut.UserId == userId && ut.UserInTaskTypeCode == 2).Count() > 0);

            if (grantModel != null)
            {
                editGrant = grantModel.IsGranted;
            }
            else
            {
                if (isFriendTask)
                {
                    editGrant = false;
                }
                else
                    editGrant = true;
            }

            RepeatingTask repeatingTask = context.RepeatingTasks.GetById(id);

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
                Multiplier = repeatingTask?.Multiplier,
                PeriodCode = repeatingTask?.PeriodCode,
                UserIds = context.UsersInTasks.GetAll().Where(ut => ut.MyTaskId == task.Id && ut.UserInTaskTypeCode == 2).Select(ut => ut.UserId).ToList(),
                files = _fileService.GetAll(id).ToList(),
                EditGrant = editGrant,
                UserId = userId,
                IsFriendTask = isFriendTask
            };
        }

        public bool Create(TaskModelBL model)
        {
            if (model == null) return false;

            string UserId = HttpContextExtensions.GetUserId(_httpcontext);

            var user = context.TaskTrackerUser.GetAll().FirstOrDefault(x => x.UserId == UserId);

            if (user == null)
            {
                user = new TaskTrackerUser()
                {
                    UserId = UserId
                };
                context.TaskTrackerUser.Create(user);
            }

            MyTask task = new MyTask
            {
                Name = model.Name,
                StartDate = DateTime.Now,
                TargetDate = model.TargetDate,
                Details = model.Details,
                IsRepeating = model.IsRepeating,
                TaskСategoryId = model.TaskСategoryId,
                TaskPriorityId = model.TaskPriorityId,
                ParentTaskId = model.ParentTaskId
            };

            context.MyTasks.Create(task);

            var UserInTask = new UsersInTask()
            {
                Task = task,
                UserId = UserId,
                TaskTrackerUser = user,
                UserInTaskTypeCode = 1
            };
            context.UsersInTasks.Create(UserInTask);

            if (model.Multiplier != null && model.PeriodCode != null)
            {
                RepeatingTask repeatingTask = new RepeatingTask
                {
                    Task = task,
                    PeriodCode = (int)model.PeriodCode,
                    Multiplier = (int)model.Multiplier
                };
                context.RepeatingTasks.Create(repeatingTask);
            }

            context.SaveChanges();

            foreach (FileModelBL fl in model.files)
                _fileService.Create(task.Id, fl);

            return true;
        }

        public async Task<bool> Update(int id, TaskModelBL model)
        {
            if (model == null) return false;

            if (!CheckAccess(id, out MyTask task)) return false;

            task.Name = model.Name;
            task.TargetDate = model.TargetDate;
            task.Details = model.Details;
            task.IsRepeating = model.IsRepeating;
            task.TaskСategoryId = model.TaskСategoryId;
            task.TaskPriorityId = model.TaskPriorityId;
            task.EndDate = model.EndDate;
            task.ParentTaskId = model.ParentTaskId;
            context.MyTasks.Update(task);

            if (model.Friends != null)
            {
                foreach (var friend in model.Friends)
                {
                    UsersInTask member = new UsersInTask
                    {
                        MyTaskId = model.Id,
                        UserId = friend.UserId,
                        UserInTaskTypeCode = 2
                    };

                    if (context.TaskTrackerUser.GetById(member.UserId) != null)
                    {
                        context.UsersInTasks.Create(member);
                    }
                    else
                    {
                        TaskTrackerUser user = new TaskTrackerUser { UserId = member.UserId };
                        context.TaskTrackerUser.Create(user);
                        if(!context.UsersInTasks.GetAll().Any(ut=>ut.UserId==member.UserId && ut.UserInTaskTypeCode==2))
                            context.UsersInTasks.Create(member);
                    }
                }
            }

            RepeatingTask repeatingTask = context.RepeatingTasks.GetById(id);

            if (repeatingTask != null && model.IsRepeating == true)
            {
                repeatingTask.Multiplier = (int)model.Multiplier;
                repeatingTask.PeriodCode = (int)model.PeriodCode;
                context.RepeatingTasks.Update(repeatingTask);
            }
            else if (repeatingTask != null && model.IsRepeating == false)
            {
                context.RepeatingTasks.Delete(repeatingTask);
            }
            else if (repeatingTask == null && model.IsRepeating == true &&
                model.Multiplier != null && model.PeriodCode != null)
            {
                repeatingTask = new RepeatingTask
                {
                    Id = model.Id,
                    Multiplier = (int)model.Multiplier,
                    PeriodCode = (int)model.PeriodCode
                };
                context.RepeatingTasks.Create(repeatingTask);
            }

            context.SaveChanges();

            var list = context.TaskFiles.GetAll().Where(tf => tf.TaskId == id).Select(tf=>tf.FileId).ToList();

            foreach (int FileId in list)
            {
                if (!model.files.Any(fl => fl.Id == FileId))
                    _fileService.Delete(FileId);
            }

            foreach (FileModelBL fl in model.files.Where(fl => !list.Any(fileid => fileid == fl.Id)))
            {
                _fileService.Create(id, fl);
            }

            if (model.Friends != null)
            {
                foreach (var _friend in model.Friends)
                {
                    await _mailService.SendAsync(new MailModelBL()
                    {
                        To = _friend.Friend.Email,
                        Subject = "Назначение соисполнителем на задачу",
                        Body = $"Вы назначены соисполнителем на задачу <br> для просмотра перейдите по ссылке: <br> <a href=\"https://localhost:44347/MyTask/Update/{model.Id}\">Перейти...</a>"
                    });
                }
            }

            return true;
        }

        public bool Delete(int id)
        {
            if (!CheckAccess(id, out MyTask task)) return false;

            _fileService.DeleteForTask(id);
            foreach (MyTask tektask in context.MyTasks.GetAll().Where(t=>t.ParentTaskId==id))
            {
                tektask.ParentTaskId = null;
                tektask.ParentTask = null;
                context.MyTasks.Update(tektask);
            }
            context.MyTasks.Delete(task);

            context.SaveChanges();

            return true;
        }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskTracker.Shared.Extensions;
using TaskTracker.BL.Interfaces;
using TaskTracker.DAL.Models;

namespace TaskTracker.BL.Services
{
    public class CheckUserAccessService : ICheckUserService
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        public CheckUserAccessService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }
        public bool AccessGrantedForUser(int taskId)
        {
            if (taskId <= 0) return false;

            string UserId = HttpContextExtensions.GetUserId(httpContextAccessor);

            if (HttpContextExtensions.IsAdmin(httpContextAccessor)) return true;
                
                var myTasks = context.UsersInTasks.Where(t => t.MyTaskId == taskId
                                                       ).Join(context.TaskTrackerUser,
                                                            t => t.UserId,
                                                            f => f.UserId,
                                                            (t, f) => new
                                                            {
                                                                f.UserId
                                                            }
                                                     ).Select(t => new { t.UserId });

            return myTasks.Any(t=>t.UserId == UserId);
        }

    }
}

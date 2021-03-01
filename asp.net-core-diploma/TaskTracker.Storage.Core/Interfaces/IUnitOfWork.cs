using System.Threading.Tasks;
using TaskTracker.Storage.Core.Entities;

namespace TaskTracker.Storage.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<MyFile, int> MyFiles { get; }
        IRepository<MyTask, int> MyTasks { get; }
        IRepository<Notification, int> Notifications { get; }
        IRepository<NotificationUser, int> NotificationUsers { get; }
        IRepository<PeriodType, int> PeriodTypes { get; }
        IRepository<PriorityType, int> PriorityTypes { get; }
        IRepository<RepeatingTask, int> RepeatingTasks { get; }
        IRepository<Tag, int> Tags { get; }
        IRepository<TaskEditGrants, int> TaskEditGrants { get; }
        IRepository<TaskFile, int> TaskFiles { get; }
        IRepository<TaskTag, int> TaskTags { get; }
        IRepository<TaskTrackerUser, string> TaskTrackerUser { get; }
        IRepository<TaskСategory, int> TaskСategories { get; }
        IRepository<UserInTaskType, int> UserInTaskTypes { get; }
        IRepository<UsersInTask, string> UsersInTasks { get; }
        
        void SaveChanges();

        Task<int> SaveChangesAsync();
    }
}

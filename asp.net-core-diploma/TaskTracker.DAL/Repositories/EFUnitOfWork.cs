using System;
using System.Threading.Tasks;
using TaskTracker.DAL.Models;
using TaskTracker.DAL.Repositories;
using TaskTracker.Storage.Core.Entities;
using TaskTracker.Storage.Core.Interfaces;

namespace TaskTracker.DAL.Repositories
{
    public class EFUnitOfWork: IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext context;
        public EFUnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }
        private IRepository<MyFile, int> myFileRepository;
        private IRepository<MyTask, int> myTasksRepository;
        private IRepository<Notification, int> notificationsRepository;
        private IRepository<NotificationUser, int> notificationUsersRepository;
        private IRepository<PeriodType, int> periodTypesRepository;
        private IRepository<PriorityType, int> priorityTypesRepository;
        private IRepository<RepeatingTask, int> repeatingTasksRepository;
        private IRepository<Tag, int> tagsRepository;
        private IRepository<TaskEditGrants, int> taskEditGrantsRepository;
        private IRepository<TaskFile, int> taskFilesRepository;
        private IRepository<TaskTag, int> taskTagsRepository;
        private IRepository<TaskTrackerUser, string> taskTrackerUserRepository;
        private IRepository<TaskСategory, int> taskСategoriesRepository;
        private IRepository<UserInTaskType, int> userInTaskTypesRepository;
        private IRepository<UsersInTask, string> usersInTasksRepository;

        public IRepository<MyFile, int> MyFiles
        {
            get
            {
                if (myFileRepository == null)
                {
                    myFileRepository = new MyFileRepository(context);
                }
                return myFileRepository;
            }
        }

        public IRepository<MyTask, int> MyTasks
        {
            get
            {
                if (myTasksRepository == null)
                {
                    myTasksRepository = new MyTaskRepository(context);
                }
                return myTasksRepository;
            }
        }

        public IRepository<Notification, int> Notifications
        {
            get
            {
                if (notificationsRepository == null)
                {
                    notificationsRepository = new NotificationRepository(context);
                }
                return notificationsRepository;
            }
        }

        public IRepository<NotificationUser, int> NotificationUsers
        {
            get
            {
                if (notificationUsersRepository == null)
                {
                    notificationUsersRepository = new NotificationUserRepository(context);
                }
                return notificationUsersRepository;
            }
        }

        public IRepository<PeriodType, int> PeriodTypes
        {
            get
            {
                if (periodTypesRepository == null)
                {
                    periodTypesRepository = new PeriodTypeRepository(context);
                }
                return periodTypesRepository;
            }
        }

        public IRepository<PriorityType, int> PriorityTypes
        {
            get
            {
                if (priorityTypesRepository == null)
                {
                    priorityTypesRepository = new PriorityTypeRepository(context);
                }
                return priorityTypesRepository;
            }
        }

        public IRepository<RepeatingTask, int> RepeatingTasks
        {
            get
            {
                if (repeatingTasksRepository == null)
                {
                    repeatingTasksRepository = new RepeatingTaskRepository(context);
                }
                return repeatingTasksRepository;
            }
        }

        public IRepository<Tag, int> Tags
        {
            get
            {
                if (tagsRepository == null)
                {
                    tagsRepository = new TagRepository(context);
                }
                return tagsRepository;
            }
        }

        public IRepository<TaskEditGrants, int> TaskEditGrants
        {
            get
            {
                if (taskEditGrantsRepository == null)
                {
                    taskEditGrantsRepository = new TaskEditGrantRepository(context);
                }
                return taskEditGrantsRepository;
            }
        }

        public IRepository<TaskFile, int> TaskFiles
        {
            get
            {
                if (taskFilesRepository == null)
                {
                    taskFilesRepository = new TaskFileRepository(context);
                }
                return taskFilesRepository;
            }
        }

        public IRepository<TaskTag, int> TaskTags
        {
            get
            {
                if (taskTagsRepository == null)
                {
                    taskTagsRepository = new TaskTagRepository(context);
                }
                return taskTagsRepository;
            }
        }

        public IRepository<TaskTrackerUser, string> TaskTrackerUser
        {
            get
            {
                if (taskTrackerUserRepository == null)
                {
                    taskTrackerUserRepository = new TaskTrackerUserRepository(context);
                }
                return taskTrackerUserRepository;
            }
        }

        public IRepository<TaskСategory, int> TaskСategories
        {
            get
            {
                if (taskСategoriesRepository == null)
                {
                    taskСategoriesRepository = new TaskCategoryRepository(context);
                }
                return taskСategoriesRepository;
            }
        }

        public IRepository<UserInTaskType, int> UserInTaskTypes
        {
            get
            {
                if (userInTaskTypesRepository == null)
                {
                    userInTaskTypesRepository = new UserInTaskTypeRepository(context);
                }
                return userInTaskTypesRepository;
            }
        }

        public IRepository<UsersInTask, string> UsersInTasks
        {
            get
            {
                if (usersInTasksRepository == null)
                {
                    usersInTasksRepository = new UsersInTaskRepository(context);
                }
                return usersInTasksRepository;
            }
        }

        public void SaveChanges()
        {
            context.SaveChanges();            
        }
        

        public void Dispose()
        {
            context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}

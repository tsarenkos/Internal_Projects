using Microsoft.EntityFrameworkCore;
using TaskTracker.Storage.Core.Entities;

namespace TaskTracker.DAL.Models
{
    public partial class ApplicationDbContext : DbContext
    {
        public DbSet<TaskTrackerUser> TaskTrackerUser { get; set; }
        public DbSet<PriorityType> Priorities { get; set; }
        public DbSet<MyFile> Files { get; set; }
        public DbSet<TaskFile> TasksFiles { get; set; }
        public DbSet<MyTask> MyTasks { get; set; }
        public DbSet<PeriodType> PeriodTypes { get; set; }
        public DbSet<RepeatingTask> RepeatingTasks { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TaskTag> TaskTags { get; set; }
        public DbSet<TaskСategory> TaskСategories { get; set; }
        public DbSet<UsersInTask> UsersInTasks { get; set; }
        public DbSet<UserInTaskType> UserInTaskTypes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationUser> NotificationUsers { get; set; }
        public DbSet<TaskEditGrants> TaskEditGrants { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=STS-PC1;Database=TaskTracker;Trusted_Connection=True;MultipleActiveResultSets=true");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MyTask>(entity =>
            {               
                entity.HasOne(t => t.Priority)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(t => t.TaskPriorityId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(t => t.TaskCategory)
                    .WithMany(tc => tc.Tasks)
                    .HasForeignKey(t => t.TaskСategoryId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(t => t.ParentTask)
                    .WithMany(t => t.SubTasks)
                    .HasForeignKey(t => t.ParentTaskId);
            });                    

            modelBuilder.Entity<TaskFile>(entity =>
            {
                entity.HasKey(e => new { e.TaskId, e.FileId });

                entity.HasOne(d => d.Task)
                    .WithMany(t => t.TaskFiles)
                    .HasForeignKey(d => d.TaskId);                    

                entity.HasOne(d => d.File)
                    .WithMany(f => f.TaskFiles)
                    .HasForeignKey(d => d.FileId);                                      
            });

            //modelBuilder.Entity<Tag>().HasAlternateKey(t => t.Name);

            modelBuilder.Entity<TaskTag>(entity =>
            {
                entity.HasKey(tt => new { tt.MyTaskId, tt.TagId });

                entity.HasOne(tt => tt.Task)
                        .WithMany(task => task.TaskTags)
                        .HasForeignKey(tt => tt.MyTaskId);

                entity.HasOne(tt => tt.Tag)
                        .WithMany(tag => tag.TaskTags)
                        .HasForeignKey(tt => tt.TagId);
            });

            modelBuilder.Entity<RepeatingTask>().Property(rt => rt.Multiplier).HasDefaultValue(1);

            modelBuilder.Entity<UsersInTask>(entity =>
            {
                entity.HasKey(uit => new { uit.UserId, uit.MyTaskId});

                entity.HasOne(uit => uit.TaskTrackerUser)
                        .WithMany(user => user.UsersInTasks)
                        .HasForeignKey(uit => uit.UserId);                        

                entity.HasOne(uit => uit.Task)
                        .WithMany(task => task.UsersInTasks)
                        .HasForeignKey(uit => uit.MyTaskId);

                entity.HasOne(uit => uit.UserInTaskType)
                        .WithMany(u => u.UsersInTasks)
                        .HasForeignKey(uit => uit.UserInTaskTypeCode)
                        .OnDelete(DeleteBehavior.SetNull);
            });            

            modelBuilder.Entity<NotificationUser>(entity =>
            {
                entity.HasKey(nus => new { nus.NotificationId, nus.UserId });

                entity.HasOne(nus => nus.Notification)
                .WithMany(n => n.NotificationUsers)
                .HasForeignKey(nus => nus.NotificationId);

            });

            modelBuilder.Entity<TaskEditGrants>().HasKey(t => new { t.TaskId, t.FriendId });

            modelBuilder.Entity<UserInTaskType>().HasData(
                           new UserInTaskType { Id = 1, Name = "владелец" },
                           new UserInTaskType { Id = 2, Name = "друг" }
                           );

            modelBuilder.Entity<PeriodType>().HasData(
                new PeriodType { Id = 1, Name = "день" },
                new PeriodType { Id = 2, Name = "неделя" },
                new PeriodType { Id = 3, Name = "месяц" },
                new PeriodType { Id = 4, Name = "год" }
                );

            modelBuilder.Entity<PriorityType>().HasData(
                new PriorityType { Id = 1, Name = "обычный" },
                new PriorityType { Id = 2, Name = "низкий" },
                new PriorityType { Id = 3, Name = "высокий" },
                new PriorityType { Id = 4, Name = "критический" }
                );

            modelBuilder.Entity<TaskСategory>().HasData(
                new TaskСategory { Id = 1, Name = "дом" },
                new TaskСategory { Id = 2, Name = "личное" },
                new TaskСategory { Id = 3, Name = "учеба" },
                new TaskСategory { Id = 4, Name = "работа" },
                new TaskСategory { Id = 5, Name = "бизнес" },
                new TaskСategory { Id = 6, Name = "прочее" }
            );

            modelBuilder.Entity<MyTask>().HasData(
                new MyTask { Id = 1, Name = "Встреча по Zoom", StartDate = new System.DateTime(2021, 02, 04, 20, 56, 0, 0), TargetDate = new System.DateTime(2021, 02, 04, 23, 56, 0, 0), Details = "Диплом", IsRepeating = true, TaskСategoryId = 1, TaskPriorityId = 4 },
                new MyTask { Id = 2, Name = "Съездить в офис", StartDate = new System.DateTime(2021, 02, 05, 14, 55, 44, 0), TargetDate = new System.DateTime(2021, 02, 09, 00, 00, 0, 0), Details = "Поработать с документами", IsRepeating = true, TaskСategoryId = 5, TaskPriorityId = 2 },
                new MyTask { Id = 3, Name = "Купить краску", StartDate = new System.DateTime(2021, 02, 06, 02, 56, 22, 0), TargetDate = new System.DateTime(2021, 02, 02, 00, 00, 0, 0), Details = "нужно покрасить стены", IsRepeating = false, TaskСategoryId = 3, TaskPriorityId = 1 },
                new MyTask { Id = 4, Name = "Покрасить стены", StartDate = new System.DateTime(2021, 02, 06, 02, 57, 22, 0), TargetDate = new System.DateTime(2021, 02, 15, 00, 00, 0, 0), Details = "Для ремонта", IsRepeating = true, TaskСategoryId = 3, TaskPriorityId = 3 },
                new MyTask { Id = 5, Name = "task2", StartDate = new System.DateTime(2021, 02, 07, 21, 02, 58, 0), TargetDate = new System.DateTime(2021, 02, 12, 00, 00, 0, 0), Details = "test2", IsRepeating = true, TaskСategoryId = 3, TaskPriorityId = 2 },
                new MyTask { Id = 6, Name = "Защита диплома", StartDate = new System.DateTime(2021, 02, 06, 21, 02, 58, 0), TargetDate = new System.DateTime(2021, 02, 12, 00, 00, 0, 0), Details = "ASP.NET Core", IsRepeating = false, TaskСategoryId = 4, TaskPriorityId = 3, ParentTaskId = 5 },
                new MyTask { Id = 7, Name = "Занятие по ASP.NET Core", StartDate = new System.DateTime(2021, 02, 07, 18, 30, 58, 0), TargetDate = new System.DateTime(2021, 02, 07, 21, 30, 0, 0), Details = "Тема - потоки", IsRepeating = false, TaskСategoryId = 2, TaskPriorityId = 3 },
                new MyTask { Id = 8, Name = "task3", StartDate = new System.DateTime(2021, 02, 03, 21, 02, 58, 0), TargetDate = new System.DateTime(2021, 02, 04, 00, 00, 0, 0), Details = "test3", IsRepeating = true, TaskСategoryId = 3, TaskPriorityId = 2, ParentTaskId = 5 },
                new MyTask { Id = 9, Name = "Задача Боба", StartDate = new System.DateTime(2021, 02, 02), TargetDate = new System.DateTime(2021, 02, 09), Details = "Печь Крабсбургеры", IsRepeating = false, TaskСategoryId = 4, TaskPriorityId = 4 },
                new MyTask { Id = 10, Name = "Задача Боба 2", StartDate = new System.DateTime(2021, 02, 03), TargetDate = new System.DateTime(2021, 02, 09), Details = "Печь много Крабсбургеров", IsRepeating = true, TaskСategoryId = 4, TaskPriorityId = 3 }
            );

            modelBuilder.Entity<RepeatingTask>().HasData(
                new RepeatingTask { Id = 1, PeriodCode = 1, Multiplier = 3 },
                new RepeatingTask { Id = 2, PeriodCode = 1, Multiplier = 4 },
                new RepeatingTask { Id = 4, PeriodCode = 1, Multiplier = 1 },
                new RepeatingTask { Id = 5, PeriodCode = 2, Multiplier = 2 }
           );

            modelBuilder.Entity<TaskTrackerUser>().HasData(
                new TaskTrackerUser { UserId = "2" },
                new TaskTrackerUser { UserId = "3" }
            );

            modelBuilder.Entity<MyFile>().HasData(
            new MyFile { Id = 1, ContentType = "image/jpeg", FileName = "crabsburger.jpg" },
            new MyFile { Id = 2, ContentType = "image/jpeg", FileName = "manycrabsburgers.jpg" }
            );

            modelBuilder.Entity<TaskFile>().HasData(
                new TaskFile { TaskId = 9, FileId = 1 },
                new TaskFile { TaskId = 10, FileId = 2 }
            );

            modelBuilder.Entity<Tag>().HasData(
                    new Tag { Id = 1, Name= "Еда"},
                    new Tag { Id = 2, Name = "Работа" },
                    new Tag { Id = 3, Name = "Обучение" },
                    new Tag { Id = 4, Name = "Офис" }
                );

            modelBuilder.Entity<TaskTag>().HasData(
        new TaskTag { TagId = 1, MyTaskId=9 },
        new TaskTag { TagId = 1, MyTaskId = 10 },
        new TaskTag { TagId = 2, MyTaskId = 1 },
        new TaskTag { TagId = 2, MyTaskId = 2 },
        new TaskTag { TagId = 3, MyTaskId = 6 },
        new TaskTag { TagId = 3, MyTaskId = 7 },
        new TaskTag { TagId = 4, MyTaskId = 1 },
        new TaskTag { TagId = 4, MyTaskId = 2 }
    );


            modelBuilder.Entity<UsersInTask>().HasData(
                new UsersInTask { UserId = "2", MyTaskId = 1, UserInTaskTypeCode = 1 },
                new UsersInTask { UserId = "2", MyTaskId = 2, UserInTaskTypeCode = 1 },
                new UsersInTask { UserId = "2", MyTaskId = 3, UserInTaskTypeCode = 1 },
                new UsersInTask { UserId = "2", MyTaskId = 4, UserInTaskTypeCode = 1 },
                new UsersInTask { UserId = "2", MyTaskId = 5, UserInTaskTypeCode = 1 },
                new UsersInTask { UserId = "2", MyTaskId = 6, UserInTaskTypeCode = 1 },
                new UsersInTask { UserId = "2", MyTaskId = 7, UserInTaskTypeCode = 1 },
                new UsersInTask { UserId = "2", MyTaskId = 8, UserInTaskTypeCode = 1 },
                new UsersInTask { UserId = "2", MyTaskId = 9, UserInTaskTypeCode = 2 },
                new UsersInTask { UserId = "3", MyTaskId = 9, UserInTaskTypeCode = 1 },
                new UsersInTask { UserId = "3", MyTaskId = 10, UserInTaskTypeCode = 1 }
            );
        }
    }
}

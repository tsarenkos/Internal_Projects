using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TaskTracker.Storage.Core.Entities
{
    public class MyTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime TargetDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Details { get; set; }
        public bool IsRepeating { get; set; }

        public int? TaskСategoryId { get; set; }
        public TaskСategory TaskCategory { get; set; }

        public int? TaskPriorityId { get; set; }
        [ForeignKey("TaskPriorityId")]
        public PriorityType Priority { get; set; }

        public int? ParentTaskId { get; set; }
        [ForeignKey("ParentTaskId")]
        public MyTask ParentTask { get; set; }
        public List<MyTask> SubTasks { get; set; }

        public List<TaskTag> TaskTags { get; set; }
        public RepeatingTask RepeatingTask { get; set; }
        public List<UsersInTask> UsersInTasks { get; set; }
        public List<Notification> Notifications { get; set; }
        public List<TaskFile> TaskFiles { get; set; }       
    }
}

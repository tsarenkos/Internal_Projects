using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Storage.Core.Entities
{
    public class UsersInTask
    {
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public TaskTrackerUser TaskTrackerUser { get; set; }

        public int MyTaskId { get; set; }

        [ForeignKey("MyTaskId")]
        public MyTask Task { get; set; }

        public int? UserInTaskTypeCode { get; set; }

        [ForeignKey("UserInTaskTypeCode")]
        public UserInTaskType UserInTaskType { get; set; }
    }
}

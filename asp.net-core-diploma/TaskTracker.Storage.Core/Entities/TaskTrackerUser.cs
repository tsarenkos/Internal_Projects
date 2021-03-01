using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Storage.Core.Entities
{
    public class TaskTrackerUser
    {        
        [Key]
        public string UserId { get; set; }
        public List<UsersInTask> UsersInTasks { get; set; }
    }
}

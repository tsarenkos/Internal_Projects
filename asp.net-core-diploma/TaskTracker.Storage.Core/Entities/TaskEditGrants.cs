using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Storage.Core.Entities
{
    public class TaskEditGrants
    {
        [Required]
        public int TaskId { get; set; }

        [ForeignKey("TaskId")]
        public virtual MyTask Task { get; set; }

        [Required]
        [MaxLength(450)]
        public string FriendId { get; set; }

        [ForeignKey("FriendId")]
        public virtual TaskTrackerUser Friend { get; set; }

        [Required]
        public DateTime date { get; set; }

        [Required]
        public bool IsGranted { get; set; }
    }
}

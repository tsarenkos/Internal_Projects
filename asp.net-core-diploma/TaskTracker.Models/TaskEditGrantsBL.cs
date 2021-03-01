using System;

namespace TaskTracker.Models
{
    public class TaskEditGrantsBL
    {
        public int TaskId { get; set; }
        public TaskModelBL Task { get; set; }

        public string FriendId { get; set; }

        public DateTime date { get; set; }

        public bool IsGranted { get; set; }
    }
}

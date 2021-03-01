using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskTracker.Storage.Core.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        public int MyTaskId { get; set; }
        public MyTask MyTask { get; set; }
        public DateTime DateOfSending { get; set; }

        public List<NotificationUser> NotificationUsers { get; set; }

    }
}

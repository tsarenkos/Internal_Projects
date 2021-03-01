using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TaskTracker.Storage.Core.Entities
{
   public class NotificationUser
    {
        public int NotificationId { get; set; }
        [ForeignKey("NotificationId")]
        public Notification Notification { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public TaskTrackerUser TaskTrackerUser { get; set; }
    }
}

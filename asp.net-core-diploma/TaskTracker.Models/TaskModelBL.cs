using System;
using System.Collections.Generic;

namespace TaskTracker.Models
{
    public class TaskModelBL
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime TargetDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Details { get; set; }
        public bool IsRepeating { get; set; }
        public int? PeriodCode { get; set; }
        public int? Multiplier { get; set; }
        public int? TaskСategoryId { get; set; }
        public int? TaskPriorityId { get; set; }        
        public int? ParentTaskId { get; set; }
        public List<FileModelBL> files { get; set; }
        public List<TaskTagModelBL> Tags { get; set; }
        public List<UserFriendBL> Friends { get; set; }
        public List<string> UserIds { get; set; }
        public bool IsFriendTask { get; set; }
        public bool? EditGrant { get; set; }
        public string UserId { get; set; }
    }
}

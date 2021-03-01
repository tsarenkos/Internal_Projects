using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskTracker.Models;

namespace TaskTracker.Web.Models
{
    public class MyTaskViewModel
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
        public List<FileInfoViewModel> files { get; set; }
        public List<TagInfoViewModel> Tags { get; set; }
        public List<SelectListItem> TagsInSelectControl { get; set; }
        public List<TaskModelBL> SubTasks { get; set; }
        [Display(Name = "TagsList")]
        public long[] TagsIds { get; set; }
        public bool IsFriendTask { get; set; }
        public bool? TaskEditGrant { get; set; } = true;
        public string UserId { get; set; }
    }
}

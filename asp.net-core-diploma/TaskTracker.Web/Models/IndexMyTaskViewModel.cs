using System;
using System.Collections.Generic;


namespace TaskTracker.Web.Models
{
    public class IndexMyTaskViewModel
    {
        public List<MyTaskViewModel> MyTasks { get; set; }
        public PageMyTaskViewModel PageViewModel { get; set; }
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
        public bool? Delay { get; set; }
        public bool? Completed { get; set; }
        public string Pattern { get; set; }
        public int? CategoryId { get; set; }
        public int? PriorityId { get; set; }
    }
}

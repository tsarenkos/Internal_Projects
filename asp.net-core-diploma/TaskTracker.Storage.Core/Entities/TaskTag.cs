using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTracker.Storage.Core.Entities
{
    public class TaskTag
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }

        public int MyTaskId { get; set; }
        public MyTask Task { get; set; }
    }
}

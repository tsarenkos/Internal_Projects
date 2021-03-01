using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskTracker.Storage.Core.Entities
{
    public class TaskСategory
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; } // home, personal, stydy, work, business, others

        public List<MyTask> Tasks { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskTracker.Storage.Core.Entities
{
    public class PeriodType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; } // day, week, month, year

        public List<RepeatingTask> RepeatingTasks { get; set; }
    }
}

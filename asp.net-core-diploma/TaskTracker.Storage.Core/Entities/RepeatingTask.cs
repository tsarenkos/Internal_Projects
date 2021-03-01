using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TaskTracker.Storage.Core.Entities
{
    public class RepeatingTask
    {
        public int Id { get; set; }
        public int PeriodCode { get; set; }        
        public int Multiplier { get; set; }

        [ForeignKey("Id")]
        public MyTask Task { get; set; }

        [ForeignKey("PeriodCode")]
        public PeriodType PeriodType { get; set; }
    }
}

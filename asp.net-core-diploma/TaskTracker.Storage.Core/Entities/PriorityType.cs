using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Storage.Core.Entities
{
    public class PriorityType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; } // trivial, minor, major, critical        
        
        public List<MyTask> Tasks { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskTracker.Storage.Core.Entities
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]        
        public string Name { get; set; }

        public List<TaskTag> TaskTags { get; set; }
    }
}

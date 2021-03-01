using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Storage.Core.Entities
{
    public class MyFile
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string ContentType { get; set; }
        [Required]
        [MaxLength(255)]
        public string FileName { get; set; }

        public List<TaskFile> TaskFiles { get; set; }
    }
}

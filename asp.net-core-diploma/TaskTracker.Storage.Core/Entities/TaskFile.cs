
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Storage.Core.Entities
{
    public class TaskFile
    {
        public int TaskId { get; set; }
        [ForeignKey("TaskId")]
        public MyTask Task { get; set; }

        public int FileId { get; set; }
        [ForeignKey("FileId")]
        public MyFile File { get; set; }        
    }
}

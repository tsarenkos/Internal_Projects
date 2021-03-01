namespace TaskTracker.Web.Models
{
    public class FileInfoViewModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int Deleted { get; set; } = 0;
    }
}

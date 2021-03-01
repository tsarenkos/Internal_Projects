using System.Collections.Generic;

namespace TaskTracker.Models
{
    public class MailModelBL
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<AttachmentFileModel> Attachments { get; set; }
    }
}

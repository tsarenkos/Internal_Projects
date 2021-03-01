using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTracker.Models
{
    public class OptionsForSMTPServer
    {
        public string serverAddress { get; set; }
        public int serverPort { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string senderName { get; set; }
        public string senderMail { get; set; }
    }
}

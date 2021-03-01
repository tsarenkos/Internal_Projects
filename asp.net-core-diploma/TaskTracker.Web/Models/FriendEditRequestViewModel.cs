using System;
using System.Collections.Generic;
using TaskTracker.Models;

namespace TaskTracker.Web.Models
{
    public class FriendEditRequestViewModel
    {
        public int TaskId { get; set; }
        public TaskModelBL Task { get; set; }

        public string FriendId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string FamilyName { get; set; }
        public string Photo { get; set; }

        public DateTime AddDate { get; set; }

        public bool IsGranted { get; set; }
    }  
}
 
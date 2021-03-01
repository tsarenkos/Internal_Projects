using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTracker.Web.Models
{
    public class MyTaskWithFriendViewModel
    {
        public int TaskId { get; set; }     
        
        public string FriendId { get; set; }
    }
}

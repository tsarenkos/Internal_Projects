using System;
using System.Collections.Generic;

namespace TaskTracker.Web.Models
{
    public class FriendViewModel
    {
        public string FriendId { get; set; }
        public DateTime AddDate { get; set; } // дата добавления в друзья (запроса)
        public string UserName { get; set; }
        public string FamilyName { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
    }
    
    public class FriendsViewModel
    {
        public List<FriendViewModel> friendRequests { get; set; }
        public List<FriendViewModel> friends { get; set; }
        public List<FriendEditRequestViewModel> editRequests { get; set; }
    }
}
 
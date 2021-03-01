using System.Collections.Generic;

namespace TaskTracker.Web.Models
{
    public class FriendRequestsViewModel
    {
        public List<FriendViewModel> myFriendRequests { get; set; }
        public List<FriendViewModel> nonfriends { get; set; }

    }
}

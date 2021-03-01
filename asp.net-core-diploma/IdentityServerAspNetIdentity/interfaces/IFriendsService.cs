using System.Collections.Generic;
using System.Threading.Tasks;
using TaskTracker.Models;

namespace IdentityServerAspNetIdentity.interfaces
{
    public interface IFriendsService
    {
        Task<IEnumerable<UserFriendBL>> GetFriends();
        Task<IEnumerable<UserFriendBL>> GetFriendsForUser(string UserId);
        Task<IEnumerable<UserProfileBL>> GetNonFriends();
        Task<IEnumerable<UserFriendBL>> GetMyFriendRequests();
        Task<IEnumerable<UserFriendBL>> GetFriendRequests();

        Task<APIResult> AddFriendRequest(UserFriendBL request);
        Task<UserFriendBL> GetFriendRequest(string FriendId);
        Task<APIResult> AcceptFriendRequest(UserFriendBL request);
        Task<APIResult> DeclineFriendRequest(UserFriendBL request);
    }
}

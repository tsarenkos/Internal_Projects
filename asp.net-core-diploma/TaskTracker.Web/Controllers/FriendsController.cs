using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.Models;
using TaskTracker.Shared.Interfaces;
using TaskTracker.Web.Interfaces;
using TaskTracker.Web.Models;

namespace TaskTracker.Web.Controllers
{
    [Authorize(AuthenticationSchemes = "TokenAuthenticationScheme")]
    public class FriendsController : Controller
    {
        private readonly ILogger<FriendsController> _logger;
        private readonly IIdentityClient _client;
        private readonly IAPIClient _taskClient;
        private readonly IConfiguration Configuration;

        public FriendsController(ILogger<FriendsController> logger, IIdentityClient client, IConfiguration Configuration, IAPIClient taskClient)
        {
            _client = client;
            _logger = logger;
            this.Configuration = Configuration;
            _taskClient = taskClient;
        }

        [HttpGet]
        [Route("friends")]
        public async Task<IActionResult> Index()
        {
            var model = new FriendsViewModel();
            var requests = await _client.Get<IEnumerable<UserFriendBL>>("api/friendrequests");
            model.friendRequests = requests.Select(fr => new FriendViewModel()
            {
                FriendId = fr.FriendId,
                AddDate = fr.AddDate,
                FamilyName = fr.Friend.FamilyName,
                Name = fr.Friend.Name,
                UserName = fr.Friend.UserName,
                Photo = fr.Friend.Photo
            }).ToList();

            var friends = await _client.Get<IEnumerable<UserFriendBL>>("api/friends");
            model.friends = friends.Select(fr => new FriendViewModel()
            {
                AddDate = fr.AddDate,
                FriendId = fr.FriendId,
                FamilyName = fr.Friend.FamilyName,
                Name = fr.Friend.Name,
                UserName = fr.Friend.UserName,
                Photo = fr.Friend.Photo
            }).ToList();

            var editRequests = await _taskClient.Get<IEnumerable<TaskEditGrantsBL>>("api/mytaskfriendgrant/list");
            model.editRequests = editRequests.Select(er => new FriendEditRequestViewModel()
            {
                TaskId = er.TaskId,
                FriendId = er.FriendId,
                FamilyName = friends.FirstOrDefault(fr => fr.FriendId == er.FriendId).Friend.FamilyName,
                Name = friends.FirstOrDefault(fr => fr.FriendId == er.FriendId).Friend.Name,
                UserName = friends.FirstOrDefault(fr => fr.FriendId == er.FriendId).Friend.UserName,
                Photo = friends.FirstOrDefault(fr => fr.FriendId == er.FriendId).Friend.Photo,
                AddDate = er.date
            }).ToList();

            return View(model);  
        }

        [HttpGet]
        [Route("friends/request")]
        public async Task<IActionResult> FriendRequest()
        {
            var friendrequest = await _client.Get<IEnumerable<UserFriendBL>>("api/myfriendrequests");
            var nonfriends = await _client.Get<IEnumerable<UserProfileBL>>("api/nonfriends");
            var model = new FriendRequestsViewModel();
            model.myFriendRequests = friendrequest.Select(fr => new FriendViewModel()
            {
                FriendId = fr.FriendId,
                AddDate = fr.AddDate,
                FamilyName = fr.Friend.FamilyName,
                Name = fr.Friend.Name,
                UserName = fr.Friend.UserName,
                Photo = fr.Friend.Photo
            }).ToList();

            model.nonfriends = nonfriends.Select(fr => new FriendViewModel()
            {
                FriendId = fr.UserId,
                FamilyName = fr.FamilyName,
                Name = fr.Name,
                UserName = fr.UserName,
                Photo = fr.Photo
            }).ToList();

            return View(model);
        }

        [HttpPost]
        [Route("friends/request/{FriendId}")]
        public async Task<IActionResult> FriendRequest(string FriendId)
        {
            var model = new UserFriendBL()
            {
                AddDate = DateTime.Now,
                IsApproved = false,
                FriendId = FriendId
            };

            await _client.Post<UserFriendBL>("api/friendrequests", model);

            return LocalRedirect("~/friends/request");
        }

        [HttpPost]
        [Route("friends/accept/{FriendId}")]
        public async Task<IActionResult> FriendRequestAccept(string FriendId)
        {
            var model = new UserFriendBL()
            {
                AddDate = DateTime.Now,
                IsApproved = true,
                FriendId = FriendId
            };

            var res = await _client.Post<UserFriendBL>("api/acceptfriendrequest", model);
            if(!res.Success)
            {
                throw new Exception(res.Error);
            }
            return LocalRedirect("~/friends");
        }

        [HttpPost]
        [Route("friends/decline/{FriendId}")]
        public async Task<IActionResult> FriendRequestDecline(string FriendId)
        {
            var model = new UserFriendBL()
            {
                AddDate = DateTime.Now,
                IsApproved = true,
                FriendId = FriendId
            };

            var res = await _client.Post<UserFriendBL>("api/declinefriendrequest", model);
            if (!res.Success)
            {
                throw new Exception(res.Error);
            }
            return LocalRedirect("~/friends");
        }

        [HttpPost]
        [Route("friends/editaccept")]
        public async Task<IActionResult> FriendEditRequestAccept(int taskId, string friendId)
        {
            var model = new TaskEditGrantsBL()
            {
                FriendId = friendId,
                TaskId = taskId
            };

            var res = await _taskClient.Put<TaskEditGrantsBL>("api/mytaskfriendgrant/accept", model.TaskId, model);
            if (!res.Success)
            {
                throw new Exception(res.Error);
            }
            return LocalRedirect("~/friends");
        }

        [HttpPost]
        [Route("friends/editdeny")]
        public async Task<IActionResult> FriendEditRequestDeny(int taskId, string friendId)
        {
            var model = new TaskEditGrantsBL()
            {
                FriendId = friendId,
                TaskId = taskId
            };

            var res = await _taskClient.Put<TaskEditGrantsBL>("api/mytaskfriendgrant/deny", model.TaskId, model);
            if (!res.Success)
            {
                throw new Exception(res.Error);
            }
            return LocalRedirect("~/friends");
        }


    }
}

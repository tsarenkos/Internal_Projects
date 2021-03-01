using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;
using IdentityServerAspNetIdentity.interfaces;
using Microsoft.Extensions.Logging;
using System;
using TaskTracker.Models;

namespace IdentityServerAspNetIdentity.Controllers
{
    [ApiController]
    [Authorize(LocalApi.PolicyName)]
    public class FriendsAPIController : ControllerBase
    {
        private readonly ILogger<FriendsAPIController> _logger;
        private readonly IFriendsService _service;

        public FriendsAPIController(IFriendsService service, ILogger<FriendsAPIController> logger)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Route("api/friends")]
        public async Task<IActionResult> GetFriends()
        {
            try
            {
                var res = await _service.GetFriends();
                return Ok(res);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "FriendsAPIController.GetFriends");
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("api/friendsforuser/{FriendId}")]
        public async Task<IActionResult> GetFriendsForUser(string FriendId)
        {
            try
            {
                var res = await _service.GetFriendsForUser(FriendId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FriendsAPIController.GetFriends");
                return new StatusCodeResult(500);
            }
        }


        [HttpGet]
        [Route("api/nonfriends")]
        public async Task<IActionResult> GetNonFriends()
        {
            try
            {
                var res = await _service.GetNonFriends();
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FriendsAPIController.GetNonFriends");
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("api/myfriendrequests")]
        public async Task<IActionResult> GetMyFriendRequests()
        {
            try
            {
                var res = await _service.GetMyFriendRequests();
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FriendsAPIController.GetMyFriendRequests");
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("api/friendrequests")]
        public async Task<IActionResult> GetFriendRequests()
        {
            try
            {
                var res = await _service.GetFriendRequests();
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FriendsAPIController.GetFriendRequests");
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        [Route("api/friendrequests")]
        public async Task<IActionResult> AddFriendRequest(UserFriendBL request)
        {
            try
            {
                var res = await _service.AddFriendRequest(request);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FriendsAPIController.AddFriendRequest");
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        [Route("api/acceptfriendrequest")]
        public async Task<IActionResult> AcceptFriendRequest(UserFriendBL request)
        {
            try
            {
                var res = await _service.AcceptFriendRequest(request);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FriendsAPIController.AcceptFriendRequest");
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        [Route("api/declinefriendrequest")]
        public async Task<IActionResult> DeclineFriendRequest(UserFriendBL request)
        {
            try
            {
                var res = await _service.DeclineFriendRequest(request);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FriendsAPIController.DeclineFriendRequest");
                return new StatusCodeResult(500);
            }
        }


    }
}

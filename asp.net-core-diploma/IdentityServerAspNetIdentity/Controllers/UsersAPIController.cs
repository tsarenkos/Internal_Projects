using IdentityServerAspNetIdentity.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TaskTracker.Models;
using static IdentityServer4.IdentityServerConstants;

namespace IdentityServerAspNetIdentity.Controllers
{
    [ApiController]
    [Authorize(LocalApi.PolicyName)]
    public class UsersAPIController : ControllerBase
    {
        private readonly ILogger<UsersAPIController> _logger;
        private readonly IUsersService _service;

        public UsersAPIController(IUsersService service, ILogger<UsersAPIController> logger)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        [Route("api/createuser")]
        public async Task<IActionResult> CreateUser(RegisterUserBL model)
        {
            var result = await _service.CreateUser(model);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/profile")]
        public async Task<ActionResult> Get()
        {
            try
            {
                var model = await _service.GetUserProfile();
                if (model != null)
                    return Ok(model);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ProfileAPIController.Set");
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("api/photo")]
        public async Task<ActionResult> Photo()
        {
            try
            {
                var model = await _service.GetUserPhoto();
                if (model != null)
                    return Ok(model);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ProfileAPIController.Photo");
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("api/photo/{FileName}")]
        public async Task<ActionResult> Photo(string FileName)
        {
            try
            {
                var model = await _service.GetUserPhoto(FileName);
                if (model != null)
                    return Ok(model);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ProfileAPIController.Photo");
                return new StatusCodeResult(500);
            }
        }



        [HttpPost]
        [Route("api/profile")]
        public async Task<ActionResult> Set(UserProfileEditBL model)
        {
            try
            {
                await _service.UpdateUserProfile(model);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ProfileAPIController.Get");
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        [Route("api/changepassword")]
        public async Task<ActionResult> ChangePassword(ChangePasswordModel model)
        {
            try
            {                
                APIResult res = await _service.ChangePassword(model);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ProfileAPIController.Get");
                return new StatusCodeResult(500);
            }
        }

    }
}

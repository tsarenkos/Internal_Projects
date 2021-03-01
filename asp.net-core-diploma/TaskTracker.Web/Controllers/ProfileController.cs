using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;
using TaskTracker.Models;
using TaskTracker.Shared.Interfaces;
using TaskTracker.Web.Models;

namespace TaskTracker.Web.Controllers
{
    [Authorize(AuthenticationSchemes = "TokenAuthenticationScheme")]
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly IIdentityClient _client;

        public ProfileController(ILogger<ProfileController> logger, IIdentityClient client)
        {
            _client = client;
            _logger = logger;
        }

        [HttpGet]
        [Route("profile")]
        public async Task<IActionResult> Update()
        {
            var model = await _client.Get<UserProfileBL>($"api/profile");
            var viewmodel = new ProfileViewModel()
            {
                Email = model.Email,
                FamilyName = model.FamilyName,
                Name = model.Name,
                Patronymic = model.Patronymic,
                telephone = model.Telephone,                
                HasPhoto = (!string.IsNullOrWhiteSpace(model.Photo))
            };
            return View(viewmodel);
        }

        [HttpGet]
        [Route("profile/photo")]
        public async Task<IActionResult> Photo()
        {
            var modelBL = await _client.Get<UserPhotoBL>($"api/photo");
            return File(modelBL.Body, modelBL.ContentType);
        }

        [HttpGet]
        [Route("profile/photo/{filename}")]
        public async Task<IActionResult> Photo(string FileName)
        {
            var modelBL = await _client.Get<UserPhotoBL>($"api/photo/{FileName}");
            return File(modelBL.Body, modelBL.ContentType);
        }



        [HttpPost]
        [Route("profile")]
        public async Task<IActionResult> Update(ProfileViewModel viewmodel, IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                var model = new UserProfileEditBL()
                {
                    Email = viewmodel.Email,
                    FamilyName = viewmodel.FamilyName,
                    Name = viewmodel.Name,
                    Patronymic = viewmodel.Patronymic,
                    Telephone = viewmodel.telephone,
                };
                model.delPhoto = viewmodel.delPhoto;
                if (viewmodel.delPhoto) viewmodel.HasPhoto = false;
                if (uploadedFile != null && viewmodel.delPhoto == false)
                {
                    using (var ms = new MemoryStream())
                    {
                        uploadedFile.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        model.PhotoData = new UserPhotoBL()
                        {
                            ContentType = uploadedFile.ContentType,
                            Body = fileBytes
                        };
                        model.Photo = uploadedFile.FileName;
                    }
                    viewmodel.HasPhoto = true;
                }
                await _client.Post("api/profile", model);
                ViewData["ok"] = "true";
            }
            return View(viewmodel);
        }
    }
}

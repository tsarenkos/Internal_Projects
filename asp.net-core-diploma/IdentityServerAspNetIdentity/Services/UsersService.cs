using IdentityServerAspNetIdentity.interfaces;
using IdentityServerAspNetIdentity.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.Models;
using System.IO;
using TaskTracker.Shared.Extensions;

namespace IdentityServerAspNetIdentity.Services
{
    public class UsersService : IUsersService
    {
        private IHttpContextAccessor _context;
        protected UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHostEnvironment _hostingEnvironment;


        public UsersService(IHttpContextAccessor context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IHostEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _hostingEnvironment = hostingEnvironment;
        }

        private async Task<ApplicationUser> GetUserAsync()
        {
            string UserName = HttpContextExtensions.GetUserName(_context);
            return await _userManager.FindByNameAsync(UserName);
        }

        public async Task<APIResult> CreateUser(RegisterUserBL model)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {               
                result = await _userManager.AddToRoleAsync(user, "User");
                if (result.Succeeded)
                {
                    return new APIResult() { Success = true };
                }
            }
            return new APIResult()
            {
                Success = false,
                ErrorCode = result.Errors.First().Code,
                Error = result.Errors.First().Description
            };
        }

        public async Task<UserProfileBL> GetUserProfile()
        {
            ApplicationUser user = await GetUserAsync();
            return new UserProfileBL()
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name,
                FamilyName = user.FamilyName,
                Patronymic = user.Patronymic,
                Telephone = user.PhoneNumber,
                Photo = user.Photo
            };
        }

        public async Task<UserPhotoBL> GetUserPhoto()
        {
            ApplicationUser user = await GetUserAsync();

            if (!string.IsNullOrWhiteSpace(user.Photo))
            {
                return new UserPhotoBL()
                {
                    ContentType = user.PhotoContentType,
                    Body = await File.ReadAllBytesAsync(_hostingEnvironment.ContentRootPath + "\\Profiles\\" + user.Photo)
                };
            }
            else
                return null;

        }

        public async Task<UserPhotoBL> GetUserPhoto(string FileName)
        {
            var users = await _userManager.GetUsersInRoleAsync("User");
            var user = users.FirstOrDefault(users => users.Photo != null && users.Photo == FileName);
            if (user == null || string.IsNullOrWhiteSpace(user.Photo))
                return null;

            return new UserPhotoBL()
            {
                    ContentType = user.PhotoContentType,
                    Body = await File.ReadAllBytesAsync(_hostingEnvironment.ContentRootPath + "\\Profiles\\" + user.Photo)
            };
        }


        public async Task<APIResult> UpdateUserProfile(UserProfileEditBL profile)
        {
            ApplicationUser user = await GetUserAsync();
            user.Email = profile.Email;
            user.NormalizedEmail = profile.Email.ToUpper();
            user.FamilyName = profile.FamilyName;
            user.Name = profile.Name;
            user.Patronymic = profile.Patronymic;
            user.PhoneNumber = profile.Telephone;
            if(profile.delPhoto && !string.IsNullOrWhiteSpace(user.Photo))
            {
                File.Delete(_hostingEnvironment.ContentRootPath + "\\Profiles\\" + user.Photo);
                user.Photo = "";
                user.PhotoContentType = "";
            }
            else
                if(profile.PhotoData!=null)
                {
                    user.Photo = user.Id.ToString() + Path.GetExtension(profile.Photo);
                    user.PhotoContentType = profile.PhotoData.ContentType;
                    string path = _hostingEnvironment.ContentRootPath + "\\Profiles\\";
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    await File.WriteAllBytesAsync(path + user.Photo, profile.PhotoData.Body);
                }
            await _userManager.UpdateAsync(user);

            return new APIResult() { Success = true };
        }

        public async Task<APIResult> ChangePassword(ChangePasswordModel model)
        {
            if(model.OldPassword==model.NewPassword)
            {
                return new APIResult() { Success = false, Error = "Старый и новый пароли совпадают." };
            }
            string UserId = HttpContextExtensions.GetUserId(_context);

            ApplicationUser user = await _userManager.FindByIdAsync(UserId);
            if (user != null)
            {
                IdentityResult result =
                    await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    return new APIResult() { Success = true };
                }
                else
                {
                    return new APIResult() { Success = false, Error = result.Errors.FirstOrDefault().Description };
                }
            }
            else
            {
                return new APIResult() { Success = false, Error = "Пользователь не найден" };
            }
        }
    }
}

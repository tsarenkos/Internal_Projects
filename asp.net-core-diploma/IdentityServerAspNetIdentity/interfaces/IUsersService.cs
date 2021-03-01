using System.Threading.Tasks;
using TaskTracker.Models;

namespace IdentityServerAspNetIdentity.interfaces
{
    public interface IUsersService
    {
        Task<APIResult> CreateUser(RegisterUserBL model);
        Task<UserProfileBL> GetUserProfile();
        Task<UserPhotoBL> GetUserPhoto();
        Task<UserPhotoBL> GetUserPhoto(string FileName);
        Task<APIResult> UpdateUserProfile(UserProfileEditBL profile);
        Task<APIResult> ChangePassword(ChangePasswordModel model);
    }
}

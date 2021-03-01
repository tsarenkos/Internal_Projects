using System.Collections.Generic;
using System.Threading.Tasks;

namespace Claims_App.IdentityServices
{
    public interface IUserService
    {
        Task AddUser(string email, string password, string country);
        Task AddUserToRole(string userId, string role);
        Task<IList<string>> GetUserRoles(string email);
    }
}

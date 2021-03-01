using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Factory.WEB.IdentityServices
{
    public interface IRoleService
    {
        Task InitRoles();
        Task AddRole(string name);
        Task<List<IdentityRole>> GetRoles();
    }
}

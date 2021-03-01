using Claims_App.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Claims_App.IdentityServices
{
    public class UserService:IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;

        public UserService(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }
        
        public async Task AddUser(string email, string password, string country)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    throw new ArgumentNullException();
                }
                await userManager.CreateAsync(new ApplicationUser { Email = email, UserName = email, NormalizedEmail = email.ToUpper(), Country = country }, password);
            }
            catch (Exception exc)
            {
                throw exc;
            }            
        } 

        public async Task AddUserToRole(string userId, string roleName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(roleName))
                {
                    throw new ArgumentNullException();
                }
                ApplicationUser user = await userManager.FindByIdAsync(userId);
                IdentityRole identityRole = context.Roles.FirstOrDefault(r => r.Name == roleName);

                if (user != null && identityRole != null)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }                        
        }

        public async Task<IList<string>> GetUserRoles(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new ArgumentNullException();
                }
                ApplicationUser user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return null;
                }
                return await userManager.GetRolesAsync(user);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            
        }
    }
}

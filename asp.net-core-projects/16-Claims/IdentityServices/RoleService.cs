using Claims_App.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Claims_App.IdentityServices
{
    public class RoleService:IRoleService
    {
        private const string adminEmail = "admin@classwork.com";        

        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task InitRoles()
        {
            if (!await context.Roles.AnyAsync<IdentityRole>())
            {
                await context.Roles.AddAsync(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
                await context.Roles.AddAsync(new IdentityRole { Name = "User", NormalizedName = "USER" });
                await context.SaveChangesAsync();

                var admin = userManager.Users.FirstOrDefault(u => u.Email == adminEmail);
                if (admin != null)
                    await userManager.AddToRoleAsync(admin, "Admin");                
            }                        
        }
        public async Task<List<IdentityRole>> GetRoles()
        {
            return await context.Roles.ToListAsync();
        }
        public async Task AddRole(string name)
        {
            try
            {
                if (name == null)
                {
                    throw new ArgumentNullException();
                }
                await context.AddAsync(new IdentityRole { Name = name, NormalizedName = name.ToUpper() });
                await context.SaveChangesAsync();
            }
            catch (Exception exc)
            {
                throw exc;
            }                     
        }

        
    }
}

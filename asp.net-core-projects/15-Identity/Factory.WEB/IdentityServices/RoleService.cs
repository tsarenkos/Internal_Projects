using Factory.WEB.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factory.WEB.IdentityServices
{
    public class RoleService: IRoleService
    {
        private const string managerEmail = "manager@factory.com";
        private const string workerEmail = "worker@factory.com";

        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public RoleService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task InitRoles()
        {
            if (!await context.Roles.AnyAsync<IdentityRole>())
            {
                await context.Roles.AddAsync(new IdentityRole { Name = "worker", NormalizedName = "WORKER" });
                await context.Roles.AddAsync(new IdentityRole { Name = "manager", NormalizedName = "MANAGER" });
                await context.SaveChangesAsync();

                var manager = userManager.Users.FirstOrDefault(u => u.Email == managerEmail);
                if (manager != null)
                    await userManager.AddToRoleAsync(manager, "manager");

                var worker = userManager.Users.FirstOrDefault(u => u.Email == workerEmail);
                if (worker != null)
                    await userManager.AddToRoleAsync(worker, "worker");
            }                        
        }

        public async Task AddRole(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException();                
            }
            await context.AddAsync(new IdentityRole { Name = name, NormalizedName = name.ToUpper() });
            await context.SaveChangesAsync();            
        }

        public async Task<List<IdentityRole>> GetRoles()
        {
            return await context.Roles.ToListAsync();
        }
    }
}

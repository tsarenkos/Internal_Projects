using IdentityServerAspNetIdentity.Data;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nito.AsyncEx.Synchronous;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity
{
    public class InitUsers
    {
        public static void Initialize(IApplicationBuilder app)
        {
            var task = Task.Run(async () => await Init(app));
            task.WaitAndUnwrapException();
        }

        public static async Task Init(IApplicationBuilder app)
        { 
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                string[] roles = new string[] { "Administrator", "User" };

                foreach (string role in roles)
                {
                    var roleStore = new RoleStore<IdentityRole>(context);

                    if (!context.Roles.Any(r => r.Name == role))
                    {
                        var r = new IdentityRole()
                        {
                            Name = role,
                            NormalizedName = role.ToUpper()
                        };
                        await roleStore.CreateAsync(r);
                    }
                }

                var userStore = new UserStore<ApplicationUser>(context);
                var password = new PasswordHasher<ApplicationUser>();


                var users = new ApplicationUser[]
                {
                    new ApplicationUser()
                    {
                        Id = "1",
                        Email = "konstantsin78@gmail.com",
                        NormalizedEmail = "KONSTANTSIN78@GMAIL.COM",
                        UserName = "Admin",
                        NormalizedUserName = "ADMIN",
                        PhoneNumber = "",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString()
                    },
                    new ApplicationUser()
                    {
                        Id = "2",
                        UserName = "Alice",
                        NormalizedUserName = "ALICE",
                        Email = "konstantsin78@gmail.com",
                        NormalizedEmail = "KONSTANTSIN78@GMAIL.COM",
                        PhoneNumber = "111",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        FamilyName = "Johnson",
                        Name = "Alice",
                        Photo = "Alice.jpg",
                        PhotoContentType = "image/jpeg",
                        SecurityStamp = Guid.NewGuid().ToString()
                    },
                    new ApplicationUser()
                    {
                        Id = "3",
                        UserName = "Bob",
                        NormalizedUserName = "BOB",
                        Email = "konstantsin78@gmail.com",
                        NormalizedEmail = "KONSTANTSIN78@GMAIL.COM",
                        PhoneNumber = "222",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        FamilyName = "Sponge",
                        Name = "Bob",
                        Photo = "Bob.jpg",
                        PhotoContentType = "image/jpeg",
                        SecurityStamp = Guid.NewGuid().ToString()
                    },
                    new ApplicationUser()
                    {
                        Id = "4",
                        UserName = "Winny",
                        NormalizedUserName = "WINNY",
                        Email = "konstantsin78@gmail.com",
                        NormalizedEmail = "KONSTANTSIN78@GMAIL.COM",
                        PhoneNumber = "999",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        FamilyName = "Pooh",
                        Name = "Winny",
                        Photo = "Winny.png",
                        PhotoContentType = "image/png",
                        SecurityStamp = Guid.NewGuid().ToString()
                    },
                    new ApplicationUser()
                    {
                        Id = "5",
                        UserName = "Dwain",
                        NormalizedUserName = "DWAIN",
                        Email = "konstantsin78@gmail.com",
                        NormalizedEmail = "KONSTANTSIN78@GMAIL.COM",
                        EmailConfirmed = true,
                        Name = "Dwain",
                        SecurityStamp = Guid.NewGuid().ToString()
                    },
                };

                foreach (ApplicationUser user in users)
                {
                    if (!context.Users.Any(u => u.UserName == user.UserName))
                    {
                        var hashed = password.HashPassword(user, user.UserName);
                        user.PasswordHash = hashed;

                        var result = await userStore.CreateAsync(user);
                        if(user.UserName=="Admin")
                            await AssignRoles(serviceScope.ServiceProvider, user.UserName, "Administrator");
                        else
                            await AssignRoles(serviceScope.ServiceProvider, user.UserName, "User");
                    }
                }

                await context.SaveChangesAsync();
            }
        }

        public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string UserName, string role)
        {
            UserManager<ApplicationUser> _userManager = services.GetService<UserManager<ApplicationUser>>();
            ApplicationUser user = await _userManager.FindByNameAsync(UserName);
            var result = await _userManager.AddToRoleAsync(user, role);

            return result;
        }

    }
}

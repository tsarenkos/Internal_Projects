using Claims_App.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Claims_App.ClaimMiddlewares
{
    public class AccessFailedCountClaim
    {
        private readonly RequestDelegate next;
        public AccessFailedCountClaim(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context, UserManager<ApplicationUser> userManager)
        {
            var principal = context.User;
            var identity = principal?.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var user = await userManager.GetUserAsync(principal);
                if (user != null)
                {
                    identity.AddClaim(new Claim("access failed count", user.AccessFailedCount.ToString()));
                }
            }
            await next(context);
        }
    }
}

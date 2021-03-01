using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using Claims_App.Data;

namespace Claims_App.ClaimMiddlewares
{
    public class PhoneExistsClaimMiddleware
    {
        private readonly RequestDelegate next;        

        public PhoneExistsClaimMiddleware(RequestDelegate next)
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
                    if (!string.IsNullOrWhiteSpace(user.PhoneNumber))
                    {
                        identity.AddClaim(CreateClaim(ClaimTypes.MobilePhone, "true"));
                    }
                    else
                    {
                        identity.AddClaim(CreateClaim(ClaimTypes.MobilePhone, "false"));
                    }
                }                                    
            }
            await next.Invoke(context);
        }

        private Claim CreateClaim(string type, string value)
        {
            return new Claim(type, value);
        }
    }
}

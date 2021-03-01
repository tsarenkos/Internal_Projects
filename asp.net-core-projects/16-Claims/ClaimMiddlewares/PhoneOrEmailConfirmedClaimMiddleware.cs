using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using Claims_App.Data;

namespace Claims_App.ClaimMiddlewares
{
    public class PhoneOrEmailConfirmedClaimMiddleware
    {
        private readonly RequestDelegate next;        

        public PhoneOrEmailConfirmedClaimMiddleware(RequestDelegate next)
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
                    if (user.EmailConfirmed || user.PhoneNumberConfirmed)
                    {
                        identity.AddClaim(CreateClaim("phone or email confirmed", "true"));
                    }
                    else
                    {
                        identity.AddClaim(CreateClaim("phone or email confirmed", "false"));
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

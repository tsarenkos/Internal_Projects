using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Claims_App.Requirements
{
    public class CountryHandler : AuthorizationHandler<CountryRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CountryRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.Country))
            {
                string country = context.User.FindFirst(c => c.Type == ClaimTypes.Country).Value;
                if(country == requirement.country)
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}

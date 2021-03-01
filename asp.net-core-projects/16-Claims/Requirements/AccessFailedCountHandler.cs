using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace Claims_App.Requirements
{
    public class AccessFailedCountHandler : AuthorizationHandler<AccessFailedCountRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AccessFailedCountRequirement requirement)
        {
            if(context.User.HasClaim(c=>c.Type=="access failed count"))
            {
                int count = Convert.ToInt32(context.User.FindFirst("access failed count").Value);
                if (count < requirement.accessFailedCount)
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}

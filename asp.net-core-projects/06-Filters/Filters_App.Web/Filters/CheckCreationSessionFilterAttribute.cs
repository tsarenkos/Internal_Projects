using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Filters_App.Web.Filters
{
    public class CheckCreationSessionFilterAttribute : Attribute, IAsyncResourceFilter
    {
        private const string keyForSession = "initialKey";
        private const string messageAboutExistence = "Session exists";

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            bool isExisted = context.HttpContext.Session.Keys.Contains(keyForSession);

            if (isExisted)
            {
                context.Result = new ContentResult { Content = messageAboutExistence };                
            }
            else
            {
                await next();
            }            
        }
    }
}

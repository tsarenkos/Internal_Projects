using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Filters_App.Web.Filters
{
    public class DeleteSessionFilterAttribute : Attribute, IAsyncResourceFilter
    {
        private const string keyForSession = "initialKey";
        private const string notFoundMessage = "Not found";

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            bool isExisted = context.HttpContext.Session.Keys.Contains(keyForSession);

            if (isExisted)
            {
                await next();
            }
            else
            {
                context.Result = new ContentResult { Content = notFoundMessage };                
            }
        }
    }
}

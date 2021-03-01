using Filters_App.Web.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Filters_App.Web.Filters
{
    public class RedirectSessionFilterAttribute : Attribute, IAsyncResourceFilter,IOrderedFilter
    {
        private const string keyForSession = "initialKey";

        public int Order { get; set; }

        private readonly IOptionsMonitor<MySessionOptions> _optionsDelegate;

        public RedirectSessionFilterAttribute(IOptionsMonitor<MySessionOptions> optionsDelegate)
        {
            _optionsDelegate = optionsDelegate;
        }        

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            if (_optionsDelegate.CurrentValue.CheckIsCreated == true)
            {
                bool isExisted = context.HttpContext.Session.Keys.Contains(keyForSession);

                if (isExisted)
                {
                    await next();
                }
                else
                {
                    context.HttpContext.Response.Redirect("/Home/CreateSession");
                }
            }
            else
            {
                await next();
            }          
        }
    }
}

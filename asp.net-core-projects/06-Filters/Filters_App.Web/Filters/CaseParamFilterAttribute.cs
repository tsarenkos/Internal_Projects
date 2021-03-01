using Filters_App.Web.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Filters_App.Web.Filters
{
    public class CaseParamFilterAttribute : Attribute, IAsyncActionFilter, IOrderedFilter
    {
        public int Order { get; set; }

        private readonly IOptionsMonitor<MySessionOptions> _optionsDelegate;
        public CaseParamFilterAttribute(IOptionsMonitor<MySessionOptions> optionsDelegate)
        {
            _optionsDelegate = optionsDelegate;
        }        

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_optionsDelegate.CurrentValue.ChangeCaseArg == true)
            {
                context.ActionArguments["arg"] = context.ActionArguments["arg"].ToString().ToUpper();
                await next();
            }
            else
            {
                await next();
            }            
        }
    }
}

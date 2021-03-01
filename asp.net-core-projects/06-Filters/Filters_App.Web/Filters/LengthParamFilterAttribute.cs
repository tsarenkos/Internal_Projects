using Filters_App.Core.Interfaces;
using Filters_App.Web.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Filters_App.Web.Filters
{
    public class LengthParamFilterAttribute : Attribute, IAsyncActionFilter,IOrderedFilter
    {
        public int Order { get; set; }

        private readonly IOptionsMonitor<MySessionOptions> _optionsDelegate;

        private readonly IChangeStringService _changeStringService;

        public LengthParamFilterAttribute(IOptionsMonitor<MySessionOptions> optionsDelegate, IChangeStringService changeStringService)
        {
            _optionsDelegate = optionsDelegate;
            _changeStringService = changeStringService;
        }        

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_optionsDelegate.CurrentValue.ChangeLengthArg == true)
            {
                string arg = (string)context.ActionArguments["arg"];

                context.ActionArguments["arg"] = _changeStringService.ChangeLength(arg, _optionsDelegate.CurrentValue.MaxLengthArg);

                await next();                
            }
            else
            {
                await next();
            }            
        }
    }
}

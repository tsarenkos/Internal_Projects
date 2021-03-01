using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;


namespace Logging_App.WEB.Filters
{
    public class LogExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;
            context.Result = new ContentResult { Content = "Exception occured, but was handled!" };
        }
    }
}

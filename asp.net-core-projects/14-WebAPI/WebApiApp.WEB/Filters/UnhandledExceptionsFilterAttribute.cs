using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;


namespace WebApiApp.WEB.Filters
{
    public class UnhandledExceptionsFilterAttribute : Attribute, IExceptionFilter
    {
        private const string Message = "Unexpected error occured. Check your request and try again!";
        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;
            
            context.Result = new ContentResult { Content = Message, StatusCode = 400 };
        }
    }
}

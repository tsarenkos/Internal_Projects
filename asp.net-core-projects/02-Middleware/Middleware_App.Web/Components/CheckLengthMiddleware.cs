﻿using Middleware_App.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Middleware_App.Web.Models;

namespace Middleware_App.Web.Components
{
    public class CheckLengthMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IOptionsMonitor<QueryOptions> optionsDelegate;
        private readonly ICheckQueryService checkService;        

        public CheckLengthMiddleware(RequestDelegate next, IOptionsMonitor<QueryOptions> optionsDelegate, ICheckQueryService checkService)
        {
            this.next = next;
            this.optionsDelegate = optionsDelegate;
            this.checkService = checkService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string query = context.Request.QueryString.Value;
            if (checkService.CheckLength(query, optionsDelegate.CurrentValue.LengthMax))
            {
                await next.Invoke(context);
            }
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(optionsDelegate.CurrentValue.LengthErrorMessage);
            }
        }
    }
}

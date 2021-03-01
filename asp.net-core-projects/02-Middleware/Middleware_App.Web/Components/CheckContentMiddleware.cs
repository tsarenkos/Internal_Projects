﻿using Middleware_App.Core.BusinessModels;
using Middleware_App.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Middleware_App.Web.Components
{
    public class CheckContentMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly QueryOptions queryPattern;
        private readonly ICheckQueryService _checkQuery;
        private const string QueryContent = "QueryString contains denied characters!";

        public CheckContentMiddleware(RequestDelegate next, IOptions<QueryOptions> options, ICheckQueryService checkQuery)
        {
            _next = next;
            queryPattern = options.Value;
            _checkQuery = checkQuery;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            string queryString = context.Request.QueryString.ToString();
            if (_checkQuery.CheckContent(queryString, queryPattern.ContentDenied))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(QueryContent);
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}

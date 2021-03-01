using Middleware_App.Core.BusinessModels;
using Middleware_App.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Middleware_App.Web.Components
{
    public class CheckLengthMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly QueryOptions queryPattern;
        private readonly ICheckQueryService _checkQuery;
        private const string QueryContent = "QueryString is too much!";

        public CheckLengthMiddleware(RequestDelegate next, IOptions<QueryOptions> options, ICheckQueryService checkQuery)
        {
            _next = next;
            queryPattern = options.Value;
            _checkQuery = checkQuery;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            int queryLength = context.Request.QueryString.ToString().Length;
            if (_checkQuery.CheckLength(queryLength, queryPattern.LengthMax))
            {
                await _next.Invoke(context);
            }
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(QueryContent);
            }
        }
    }
}

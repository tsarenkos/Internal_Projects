using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware_App.Web.Components
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCheckQueryLength(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CheckLengthMiddleware>();
        }

        public static IApplicationBuilder UseCheckQueryContent(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CheckContentMiddleware>();
        }
    }
}

using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace TaskTracker.Shared.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetUserId(IHttpContextAccessor accessor)
        {
            return accessor.HttpContext.User?.Claims?.Where(a => a.Type == "userid").FirstOrDefault()?.Value;
        }

        public static bool IsAdmin(IHttpContextAccessor accessor)
        {
            bool IsAdmin = false;
            bool.TryParse(accessor.HttpContext.User?.Claims?.Where(a => a.Type == "admin").FirstOrDefault()?.Value, out IsAdmin);
            return IsAdmin;
        }

        public static string GetUserName(IHttpContextAccessor accessor)
        {
            return accessor.HttpContext.User?.Claims?.Where(a => a.Type == "user").FirstOrDefault()?.Value;
        }


    }
}

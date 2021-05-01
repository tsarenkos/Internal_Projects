using Middleware_App.Core.Interfaces;

namespace Middleware_App.Core.Services
{
    public class CheckQueryService:ICheckQueryService
    {
        public bool CheckLength(string queryString, int queryMax)
        {
            return queryString.Length < queryMax;
        }

        public bool CheckContent(string queryString, string queryDenied)
        {
            return !queryString.Contains(queryDenied);
        }
    }
}

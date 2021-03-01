using Middleware_App.Core.Interfaces;

namespace Middleware_App.Core.BusinessServices
{
    public class CheckQueryService:ICheckQueryService
    {
        public bool CheckLength(int queryLength, int queryMax)
        {
            if (queryLength > queryMax)
            {
                return false;
            }
            return true;
        }

        public bool CheckContent(string queryString, string queryDenied)
        {
            if (queryString==null || queryDenied==null || !queryString.Contains(queryDenied))
            {
                return false;
            }
            return true;
        }
    }
}

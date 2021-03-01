namespace Middleware_App.Core.Interfaces
{
    public interface ICheckQueryService
    {
        bool CheckLength(int queryLength, int queryMax);
        bool CheckContent(string queryString, string queryDenied);
    }
}

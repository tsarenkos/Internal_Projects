namespace Middleware_App.Core.Interfaces
{
    public interface ICheckQueryService
    {
        bool CheckLength(string queryString, int queryMax);
        bool CheckContent(string queryString, string queryDenied);
    }
}

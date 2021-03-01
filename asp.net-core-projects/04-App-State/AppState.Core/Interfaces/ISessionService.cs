using System.Collections.Generic;

namespace AppState.Core.Interfaces
{
    public interface ISessionService
    {
        IDictionary<string,string> GetAll();
        string GetByKey(string key);
        bool SetKey(string key, string value);
        bool DeleteKey(string key);
    }
}

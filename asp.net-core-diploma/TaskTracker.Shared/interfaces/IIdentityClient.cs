using System.Threading.Tasks;
using TaskTracker.Models;

namespace TaskTracker.Shared.Interfaces
{
    public interface IIdentityClient
    {
        Task<T> Get<T>(string url);
        Task<T> GetWithToken<T>(string url, string token);
        Task<APIResult> Post<T>(string url, T model);
        Task<APIResult> Put<T>(string url, int id, T model);
        Task<APIResult> Delete(string url, int id);
        Task<APIResult> CreateUser<T>(string url, T model, string token);
    }
}

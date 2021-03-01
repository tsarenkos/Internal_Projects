using System.Threading.Tasks;
using TaskTracker.Models;

namespace TaskTracker.Web.Interfaces
{
    public interface IAPIClient
    {
        public Task<T> Get<T>(string url);
        public Task<APIResult> Post<T>(string url, T model);
        public Task<APIResult> Put<T>(string url, int id, T model);
        public Task<APIResult> Delete(string url, int id);
    }
}

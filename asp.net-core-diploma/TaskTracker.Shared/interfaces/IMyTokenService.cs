using System.Threading.Tasks;
using TaskTracker.Models;

namespace TaskTracker.Shared.Interfaces
{
    public interface IMyTokenService
    {
        Task<APIToken> GetAPIToken(LoginViewModel user);
        Task<APIToken> GetIdentityToken(LoginViewModel user);
        Task<string> GetRefreshToken();
        void ClearTokens();
    }
}

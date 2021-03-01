using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TaskTracker.Shared.Interfaces;
using TaskTracker.Models;
using System;

namespace TaskTracker.Shared.Services
{
    public class MyTokenService: IMyTokenService
    {
        private readonly IHttpContextAccessor _context;
        private readonly IConfiguration Configuration;
        private readonly ILogger<MyTokenService> _logger;

        public MyTokenService(ILogger<MyTokenService> logger, IHttpContextAccessor context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            Configuration = configuration;
        }

        private async Task<string> GetTokenEndPoint()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(Configuration["IdentityServer"]);
            if (disco.IsError)
            {
                throw new Exception(disco.Error);
            }
            return disco.TokenEndpoint;
        }

        private async Task<APIToken> GetToken(PasswordTokenRequest req)
        {
            var client = new HttpClient();
            var tokenResponse = await client.RequestPasswordTokenAsync(req);

            if (tokenResponse.IsError)
            {
                return new APIToken() { Success = false, Error = tokenResponse.Error };
            };

            _context.HttpContext.Response.Cookies.Append(req.ClientId, tokenResponse.AccessToken, new CookieOptions() { Expires = new DateTimeOffset(DateTime.Now.AddHours(1)) });
            return new APIToken() { token = tokenResponse.AccessToken, Success = true };
        }

        public async Task<APIToken> GetAPIToken(LoginViewModel user)
        {
            string tokenEndPoint = await GetTokenEndPoint();
            var req = new PasswordTokenRequest
            {
                Address = tokenEndPoint,

                ClientId = "api.client",
                ClientSecret = "secret",
                Scope = "api1",

                UserName = user.UserName,
                Password = user.Password,                
            };
            APIToken token = await GetToken(req);
            _context.HttpContext.Response.Cookies.Append("UserName", user.UserName);
            return token;
        }

        public async Task<APIToken> GetIdentityToken(LoginViewModel user)
        {
            string tokenEndPoint = await GetTokenEndPoint();
            var req = new PasswordTokenRequest
            {
                Address = tokenEndPoint,

                ClientId = "identity.client",
                ClientSecret = "anothersecret",
                Scope = "IdentityServerApi",

                UserName = user.UserName,
                Password = user.Password
            };
            return await GetToken(req);
        }

        public async Task<string> GetRefreshToken()
        {
            string tokenEndPoint = await GetTokenEndPoint();
            var req = new RefreshTokenRequest
            {
                Address = tokenEndPoint,

                ClientId = "identity.client",
                ClientSecret = "anothersecret",
                Scope = "IdentityServerApi"
            };
            var client = new HttpClient();
            var tokenResponse = await client.RequestRefreshTokenAsync(req);

            if (tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            };

            _context.HttpContext.Response.Cookies.Append(req.ClientId, tokenResponse.AccessToken, new CookieOptions() { Expires = new DateTimeOffset(DateTime.Now.AddHours(1)) });
            return tokenResponse.AccessToken;
        }

        public void ClearTokens()
        {
            _context.HttpContext.Response.Cookies.Delete("identity.client");
            _context.HttpContext.Response.Cookies.Delete("api.client");
        }

    }
}

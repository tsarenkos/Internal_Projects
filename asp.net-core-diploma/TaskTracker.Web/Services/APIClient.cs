using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

using System.Threading.Tasks;
using TaskTracker.Web.Interfaces;
using TaskTracker.Models;

namespace TaskTracker.Web.Services
{
    public class APIClient: IAPIClient
    {
        private readonly ILogger<APIClient> _logger;
        protected readonly IHttpContextAccessor _context;
        private readonly IConfiguration Configuration;
        protected string _Authority;
        protected string _TokenName;

        public APIClient(ILogger<APIClient> logger, IHttpContextAccessor context, IConfiguration Configuration)
        {
            _logger = logger;
            _context = context;
            this.Configuration = Configuration;
            _Authority = Configuration["APIServer"];
            _TokenName = "api.client";
        }

        public async Task<T> Get<T>(string url)
        {
            if (_context.HttpContext.Request.Cookies.ContainsKey(_TokenName))
            {
                var apiClient = new HttpClient();
                apiClient.SetBearerToken(_context.HttpContext.Request.Cookies[_TokenName]);

                var response = await apiClient.GetAsync(string.Format("{0}/{1}", _Authority, url));
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(string.Format("Метод API \"GET {0}\" вернул HTTP {1}", url, response.StatusCode));
                }
                else
                {
                    var res = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(res);
                }
            }
            else
                throw new Exception("Не пройдена авторизация!");
        }

        public async Task<APIResult> Post<T>(string url, T model)
        {
            if (_context.HttpContext.Request.Cookies.ContainsKey(_TokenName))
            {
                var apiClient = new HttpClient();
                apiClient.SetBearerToken(_context.HttpContext.Request.Cookies[_TokenName]);

                var myContent = JsonConvert.SerializeObject(model);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await apiClient.PostAsync(string.Format("{0}/{1}", _Authority, url), byteContent);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(string.Format("Метод API \"POST {0}\" вернул HTTP {1}", url, response.StatusCode));
                }
                var res = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<APIResult>(res);
            }
            else
                throw new Exception("Не пройдена авторизация!");
        }

        public async Task<APIResult> Put<T>(string url, int id, T model)
        {
            if (_context.HttpContext.Request.Cookies.ContainsKey(_TokenName))
            {
                var apiClient = new HttpClient();
                apiClient.SetBearerToken(_context.HttpContext.Request.Cookies[_TokenName]);

                var myContent = JsonConvert.SerializeObject(model);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await apiClient.PutAsync(string.Format("{0}/{1}/{2}", _Authority, url, id), byteContent);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(string.Format("Метод API \"PUT {0}\" вернул HTTP {1}", url, response.StatusCode));
                }
                var res = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<APIResult>(res);
            }
            else
                throw new Exception("Не пройдена авторизация!");
        }

        public async Task<APIResult> Delete(string url, int id)
        {
            if (_context.HttpContext.Request.Cookies.ContainsKey(_TokenName))
            {
                var apiClient = new HttpClient();
                apiClient.SetBearerToken(_context.HttpContext.Request.Cookies[_TokenName]);

                var response = await apiClient.DeleteAsync(string.Format("{0}/{1}/{2}", _Authority, url, id));
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(string.Format("Метод API \"DELETE {0}\" вернул HTTP {1}", url, response.StatusCode));
                }
                var res = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<APIResult>(res);
            }
            else
                throw new Exception("Не пройдена авторизация!");
        }

    }
}

using AppState.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AppState.Infrastructure.Services
{
    public class SessionService:ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private const string GettingError = "Not Found!";        

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IDictionary<string,string> GetAll()
        {            
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

            IEnumerable<string> keys = _httpContextAccessor.HttpContext.Session.Keys;              
            
            foreach (string key in keys)
            {
                keyValuePairs.Add(key, _httpContextAccessor.HttpContext.Session.GetString(key));                
            }
            return keyValuePairs;
        }
        public string GetByKey(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException();
            }

            string value = _httpContextAccessor.HttpContext.Session.GetString(key);
            if (value != null)
            {
                return value;
            }
            else
            {
                return GettingError;
            }
        }
        public bool SetKey(string key, string value)
        {
            if (key == null || value == null)
            {
                throw new ArgumentNullException();
            }

            _httpContextAccessor.HttpContext.Session.SetString(key, value);
            return true;
        }
        public bool DeleteKey(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException();
            }

            if (_httpContextAccessor.HttpContext.Session.Keys.Contains(key))
            {
                _httpContextAccessor.HttpContext.Session.Remove(key);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

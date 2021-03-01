using AppState.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppState.Web.Controllers
{
    public class SessionController : Controller
    {
        private const string GettingError = "Not found!";
        private const string SetSuccess = "Key has been set!";
        private const string SetError = "Setting failed!";
        private const string DeleteSuccess = "Key was deleted!";
        private const string DeleteError = "Removing failed!";

        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        public IActionResult GetAll()
        {            
            IDictionary<string,string> keyValuePairs = _sessionService.GetAll();

            if (keyValuePairs == null)
            {
                return Content(GettingError);
            }

            StringBuilder builder = new StringBuilder();

            foreach(var key in keyValuePairs.Keys)
            {
                builder.Append(key + " - " + keyValuePairs[key] + "\n");
            }
            
            return Content(builder.ToString());
        }
        public IActionResult GetByKey(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException();
            }
            string value = _sessionService.GetByKey(key);

            if (value != null)
            {
                return Content(value);
            }
            else
            {
                return Content(GettingError);
            }
        }
        public IActionResult SetKey(string key, string value)
        {
            if (key == null || value == null)
            {
                throw new ArgumentNullException();
            }
            if (_sessionService.SetKey(key, value))
            {
                return Content(SetSuccess);
            }
            else
            {
                return Content(SetError);
            }
        }
        public IActionResult DeleteKey(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException();
            }
            if (_sessionService.DeleteKey(key))
            {
                return Content(DeleteSuccess);
            }
            else
            {
                return Content(DeleteError);
            }
        }
    }
}

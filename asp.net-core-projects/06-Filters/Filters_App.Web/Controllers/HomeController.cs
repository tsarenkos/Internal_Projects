using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Filters_App.Web.Filters;

namespace Filters_App.Web.Controllers
{
    public class HomeController : Controller
    {
        private const string keyForSession = "initialKey";
        private const string valueForKey = "initialValue";
        private const string messageAboutCreation = "Session has been created";        
        private const string deleteMessage = "Session has been deleted";

        [CheckCreationSessionFilter]
        public IActionResult CreateSession()
        {
            HttpContext.Session.SetString(keyForSession, valueForKey);
            return Content(messageAboutCreation);
        }

        [DeleteSessionFilter]
        public IActionResult DeleteSession()
        {
            HttpContext.Session.Remove(keyForSession);
            return Content(deleteMessage);
        }

        [TypeFilter(typeof(RedirectSessionFilterAttribute),Order =1)]
        [TypeFilter(typeof(LengthParamFilterAttribute),Order =2)]
        [TypeFilter(typeof(CaseParamFilterAttribute),Order =3)]
        public IActionResult SessionInfo(string arg)
        {
            return Content(arg + " " + DateTime.Now.ToString());            
        }
    }
}

using ErrorHandling_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ErrorHandling_App.Controllers
{
    public class ErrorController : Controller
    {
        private readonly IOptionsMonitor<ErrorOptions> _optionsDelegate;
        public ErrorController(IOptionsMonitor<ErrorOptions> optionsDelegate)
        {
            _optionsDelegate = optionsDelegate;
        }

        [Route("/404")]
        public IActionResult Error404()
        {
            ViewBag.Message = _optionsDelegate.CurrentValue.MessageHttp404;
            return View();
        }

        [Route("/{code:int}")]
        public IActionResult ErrorOthers(int statusCode)
        {
            ViewData["Code"] = statusCode;
            ViewData["Message"] = _optionsDelegate.CurrentValue.MessageHttpOthers;
            
            return View();
        }

    }
}

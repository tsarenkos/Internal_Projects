using Microsoft.AspNetCore.Mvc;

namespace Routing_App.Controllers
{
    public class PageController : Controller
    {
        [Route("sheets/{pageName:maxlength(5)}")]
        [Route("pages/{pageName:maxlength(5)}")]
        public IActionResult Get(string pageName)
        {
            return Content(pageName);
        }
    }
}

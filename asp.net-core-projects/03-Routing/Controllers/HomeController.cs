using Microsoft.AspNetCore.Mvc;

namespace Routing_App.Controllers
{
    public class HomeController : Controller
    {        
        public IActionResult Custom()
        {
            return Content("Custom");
        }        
    }
}

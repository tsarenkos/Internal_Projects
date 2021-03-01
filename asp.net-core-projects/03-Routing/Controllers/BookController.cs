using Microsoft.AspNetCore.Mvc;

namespace Routing_App.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index(int bookId)
        {
            return Content(bookId.ToString());
        }
    }
}

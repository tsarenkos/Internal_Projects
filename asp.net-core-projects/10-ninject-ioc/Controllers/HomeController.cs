using Ninject_App.Services;
using System.Web.Mvc;

namespace Ninject_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDateTimeWithOffsetService _dateTimeWithOffsetService;
        public HomeController(IDateTimeWithOffsetService dateTimeWithOffsetService)
        {
            _dateTimeWithOffsetService = dateTimeWithOffsetService;
        }        

        [Route("time/{offset:double}")]
        public ActionResult GetTime(double offset)
        {
            return Content(_dateTimeWithOffsetService.GetDateTime(offset).ToString());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
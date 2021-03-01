using FactoryApp.BLL.BusinessModels;
using FactoryApp.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace FactoryApp.Web.Controllers
{
    public class DelivererController : Controller
    {
        private readonly IDelivererService delivererService; 

        public DelivererController(IDelivererService delivererService)
        {
            this.delivererService = delivererService;
        }
        
        [HttpGet]
        public IActionResult AddDeliverer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddDeliverer(DelivererModel deliverer)
        {
            if (ModelState.IsValid)
            {
                delivererService.AddDeliverer(deliverer);
                return RedirectToAction("GetMachines", "Machine");
            }
            else
            {
                return BadRequest();
            }
        }        
    }
}

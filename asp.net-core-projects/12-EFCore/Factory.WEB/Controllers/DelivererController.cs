using Factory.BLL.BusinessModels;
using Factory.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Factory.WEB.Controllers
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
            }            
            return RedirectToAction("GetMachines", "Machine");
        }
    }
}

using System;
using Factory.BLL.BusinessModels;
using Factory.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.WEB.Controllers
{
    public class FactoryController : Controller
    {
        private readonly IFactoryService factoryService;
        public FactoryController(IFactoryService factoryService)
        {
            this.factoryService = factoryService;
        }
        public IActionResult GetEmployees()
        {            
            return View(factoryService.GetAllEmployees());
        }
        public IActionResult GetMachines()
        {
            return View(factoryService.GetAllMachines());
        }
        public IActionResult AddDeliverer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddDeliverer(DelivererModel deliverer)
        {
            if (deliverer == null)
            {
                throw new ArgumentNullException();
            }
            factoryService.AddDeliverer(deliverer);
            return RedirectToAction("GetMachines");
        }

        public IActionResult AddRequest()
        {
            ViewBag.Machines = new SelectList(factoryService.GetAllMachines(), "MachineId", "MachineName");
            return View();
        }

        [HttpPost]
        public IActionResult AddRequest(RequestModel req)
        {
            if (req == null)
            {
                throw new ArgumentNullException();
            }
            factoryService.AddRequest(req);
            return RedirectToAction("GetMachines");
        }

        //параметры для метода - из query string
        public IActionResult AddRequestHandler(int requestId, int employeeId)
        {
            if(requestId == 0 || employeeId == 0)
            {
                throw new ArgumentNullException();
            }
            factoryService.AddRequestHandler(requestId, employeeId);

            return RedirectToAction("GetMachines");
        }
    }
}

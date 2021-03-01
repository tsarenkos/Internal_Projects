using Factory.BLL.BusinessModels;
using Factory.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.WEB.Controllers
{
    public class RequestController : Controller
    {
        private readonly IRequestService requestService;
        private readonly IMachineService machineService;
        public RequestController(IRequestService requestService, IMachineService machineService)
        {
            this.requestService = requestService;
            this.machineService = machineService;
        }       

        [HttpGet]
        public IActionResult AddRequest()
        {
            ViewBag.Machines = new SelectList(machineService.GetAllMachines(), "MachineId", "MachineName");
            return View();
        }

        [HttpPost]
        public IActionResult AddRequest(RequestModel req)
        {
            if (ModelState.IsValid)
            {
                requestService.AddRequest(req);
            }            
            return RedirectToAction("GetMachines", "Machine");
        }

        //параметры для метода - из query string
        public IActionResult AddRequestHandler(int requestId, int employeeId)
        {
            if (requestId <= 0 || employeeId <= 0)
            {
                return BadRequest();
            }
            requestService.AddRequestHandler(requestId, employeeId);
            return RedirectToAction("GetMachines", "Machine");
        }
    }
}

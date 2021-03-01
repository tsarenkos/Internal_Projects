using System;
using System.Collections.Generic;
using Factory.BLL.BusinessModels;
using Factory.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.WEB.Controllers
{
    public class FactoryController : Controller
    {
        private readonly IEmployeesService employeesService;
        private readonly IMachinesService machinesService;
        private readonly IRequestService requestService;
        private readonly IDelivererService delivererService;

        public FactoryController(IEmployeesService employeesService, IMachinesService machinesService, IRequestService requestService,
            IDelivererService delivererService)
        {
            this.employeesService = employeesService;
            this.machinesService = machinesService;
            this.requestService = requestService;
            this.delivererService = delivererService;
        }
        public IActionResult GetEmployees()
        {
            List<EmployeeModel> employees = employeesService.GetAllEmployees();
            if (employees == null)
            {
                return NotFound();
            }
            return View(employees);
            
        }
        public IActionResult GetMachines()
        {
            List<MachineModel> machines = machinesService.GetAllMachines();
            if (machines == null)
            {
                return NotFound();
            }
            return View(machines);
        }

        [Authorize(Roles ="manager")]
        public IActionResult AddDeliverer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddDeliverer(DelivererModel deliverer)
        {
            if (deliverer == null)
            {
                return BadRequest();
            }
            delivererService.AddDeliverer(deliverer);
            return RedirectToAction("Index","Home");
        }

        public IActionResult AddRequest()
        {
            ViewBag.Machines = new SelectList(machinesService.GetAllMachines(), "MachineId", "MachineName");
            return View();
        }

        [HttpPost]
        public IActionResult AddRequest(RequestModel req)
        {
            if (req == null)
            {
                return BadRequest();
            }
            requestService.AddRequest(req);
            return RedirectToAction("Index", "Home");
        }

        //параметры для метода - из query string
        public IActionResult AddRequestHandler(int requestId, int employeeId)
        {
            if(requestId <= 0 || employeeId <= 0)
            {
                return BadRequest();
            }
            requestService.AddRequestHandler(requestId, employeeId);

            return RedirectToAction("Index", "Home");
        }
    }
}

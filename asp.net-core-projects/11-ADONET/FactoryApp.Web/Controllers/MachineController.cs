using FactoryApp.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FactoryApp.Web.Controllers
{
    public class MachineController : Controller
    {
        private readonly IMachineService machineService; 

        public MachineController(IMachineService machineService)
        {
            this.machineService = machineService;
        }        

        public IActionResult GetMachines()
        {
            return View(machineService.GetAllMachines());
        }
    }
}

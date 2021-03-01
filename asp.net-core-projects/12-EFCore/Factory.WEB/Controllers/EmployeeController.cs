using Factory.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Factory.WEB.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        public IActionResult GetEmployees()
        {            
            return View(employeeService.GetAllEmployees());
        }        
    }
}

using FactoryApp.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace FactoryApp.Web.Controllers
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

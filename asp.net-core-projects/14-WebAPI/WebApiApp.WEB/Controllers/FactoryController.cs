using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using WebApiApp.BLL.BusinessModels;
using WebApiApp.BLL.Interfaces;
using WebApiApp.WEB.Filters;

namespace WebApiApp.WEB.Controllers
{
    [UnhandledExceptionsFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class FactoryController : ControllerBase
    {
        private IFactoryService factoryService;

        public FactoryController(IFactoryService factoryService)
        {
            this.factoryService = factoryService;
        }

        [HttpGet]
        public IStatusCodeActionResult GetAll()
        {
            List<EmployeeModel> employees = factoryService.GetAll();

            if (employees == null)
            {
                return NotFound();
            }
            return new OkObjectResult(employees);
        }

        [HttpGet("{id}")]
        public IStatusCodeActionResult Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            EmployeeModel model = factoryService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            return new OkObjectResult(model);
        }

        [HttpPost]
        public IStatusCodeActionResult Create(EmployeeModel item)
        {
            if(item == null)
            {
                return BadRequest();
            }

            factoryService.Create(item);
            
            return new OkObjectResult(item);
        }

        [HttpPut]
        public IStatusCodeActionResult Update(EmployeeModel item)
        {
            if (item == null)
            {
                return BadRequest();
            }                

            factoryService.Update(item);

            return new OkObjectResult(item);
        }

        [HttpDelete("{id}")]
        public ActionResult<EmployeeModel> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            factoryService.Delete(id);

            return RedirectToAction("GetAll");
        }
    }
}

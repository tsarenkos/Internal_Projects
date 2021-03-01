using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TaskTracker.BL.Interfaces;
using TaskTracker.Models;

namespace TaskTracker.API.Controllers
{
    [ApiController]
    [Authorize(Policy = "ApiScope")]
    public class MyTaskAPIController : ControllerBase
    {
        private readonly ILogger<MyTaskAPIController> _logger;
        private readonly ITaskService taskService;

        public MyTaskAPIController(ILogger<MyTaskAPIController> logger, ITaskService taskService)
        {
            this.taskService = taskService;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/mytask/list")]
        public ActionResult Index()
        {
            try
            {
                return Ok(taskService.GetAll());
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "MyTaskAPIController.Index");
                return new StatusCodeResult(500);
            }
        }    

        [HttpPost]
        [Route("api/mytask/create")]
        public ActionResult Create(TaskModelBL task)
        {
            try
            {
                if (taskService.Create(task))
                {
                    return Ok(new APIResult() {Success = true});
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MyTaskAPIController.Create");
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("api/mytask/details/{id}")]
        public ActionResult Details(int id)
        {
            try
            {
                TaskModelBL model = taskService.GetById(id);
                if (model != null)
                    return Ok(model);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MyTaskAPIController.Details");
                return new StatusCodeResult(500);
            }
        }

        [HttpPut]
        [Route("api/mytask/update/{id}")]
        public async Task<ActionResult> Update(int id, TaskModelBL task)
        {
            try
            {
                if (await taskService.Update(id, task))
                    return Ok(new APIResult() { Success = true });
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MyTaskAPIController.Update");
                return new StatusCodeResult(500);
            }
        }

        [HttpDelete]
        [Route("api/mytask/delete/{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                if (taskService.Delete(id))
                    return Ok(new APIResult() { Success = true });
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MyTaskAPIController.Delete");
                return new StatusCodeResult(500);
            }
        }

    }
}

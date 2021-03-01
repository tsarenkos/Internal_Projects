using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.BL.Interfaces;

namespace TaskTracker.API.Controllers
{
    [ApiController]
    [Authorize(Policy = "ApiScope")]
    public class SubTaskAPIController : ControllerBase
    {
        private readonly ILogger<SubTaskAPIController> _logger;
        private readonly ISubTaskService taskService;

        public SubTaskAPIController(ILogger<SubTaskAPIController> logger, ISubTaskService taskService)
        {
            this.taskService = taskService;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/subtasks/{ParentTaskId}")]
        public ActionResult Index(int ParentTaskId)
        {
            try
            {
                return Ok(taskService.GetNonSubTask(ParentTaskId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SubTaskAPIController.Index");
                return new StatusCodeResult(500);
            }
        }

    }
}

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
    public class TaskFriendGrantsAPIController : ControllerBase
    {
        private readonly ILogger<TaskFriendGrantsAPIController> _logger;
        private readonly ITaskFriendGrants taskService;

        public TaskFriendGrantsAPIController(ILogger<TaskFriendGrantsAPIController> logger, ITaskFriendGrants taskService)
        {
            this.taskService = taskService;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/mytaskfriendgrant/list")]
        public ActionResult Index()
        {
            try
            {
                return Ok(taskService.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TaskFriendGrantsController.Index");
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        [Route("api/mytaskfriendgrant/create")]
        public async Task<ActionResult> Create(TaskEditGrantsBL request)
        {
            try
            {
                var res = await taskService.Create(request);
                if (!res.Success)
                    return Ok(res);

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TaskFriendGrantsController.Create");
                return new StatusCodeResult(500);
            }
        }

        [HttpPut]
        [Route("api/mytaskfriendgrant/accept/{id}")]
        public async Task<ActionResult> Accept(int id, TaskEditGrantsBL task)
        {
            try
            {
                var res = await taskService.Grant(task);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TaskFriendGrantsController.Update");
                return new StatusCodeResult(500);
            }
        }

        [HttpPut]
        [Route("api/mytaskfriendgrant/deny/{id}")]
        public async Task<ActionResult> Deny(int id, TaskEditGrantsBL task)
        {
            try
            {
                var res = await taskService.Deny(task);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MyTaskAPIController.Delete");
                return new StatusCodeResult(500);
            }
        }


    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using TaskTracker.BL.Interfaces;
using TaskTracker.Models;

namespace TaskTracker.API.Controllers
{

    [ApiController]
   [Authorize(Policy = "ApiScope")]
    public class TaskTagController : ControllerBase
    {
        private readonly ILogger<TaskTagController> _logger;
        private readonly ITaskTagServiceBL _service;

        public TaskTagController(ILogger<TaskTagController> logger, ITaskTagServiceBL service)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/tasktag/list")]
        public ActionResult Index()
        {
            try
            {
                return Ok(_service.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TaskTagController.Index");
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("api/tasktag/details/{id}")]
        public ActionResult GetTagById(int id)
        {
            try
            {
                var res = _service.GetTagById(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TaskTagController.GetTagById");
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("api/tasktag/fortask/{taskId}")]
        public ActionResult GetTagByTaskId(int taskId)
        {
            try
            {
                var res = _service.GetTagByTaskId(taskId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TaskTagController.GetTagByTaskId");
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        [Route("api/tasktag/create/{taskId}")]
        public ActionResult Create(int taskId, [FromBody]string tagName)
        {
            try
            {
                _service.Add(taskId, tagName);
                return Ok(new APIResult() { Success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TaskTagController.Create");
                return new StatusCodeResult(500);
            }
        }

        [HttpPut]
        [Route("api/tasktag/update/{tagId}")]
        public ActionResult Update(int tagId, [FromBody] string tagName)
        {
            try
            {
                _service.Edit(tagId, tagName);
                return Ok(new APIResult() { Success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TaskTagController.Create");
                return new StatusCodeResult(500);
            }
        }

        [HttpDelete]
        [Route("api/tasktag/delete/{tagId}")]
        public ActionResult Delete(int tagId)
        {
            try
            {
                _service.Delete(tagId);
                return Ok(new APIResult() { Success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TaskTagController.Delete");
                return new StatusCodeResult(500);
            }
        }

    }
}
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskTracker.BL.Interfaces;
using TaskTracker.Models;

namespace TaskTracker.API.Controllers
{
    [ApiController]
    [Authorize(Policy = "ApiScope")]
    public class RepeatingTaskAPIController : ControllerBase
    {
        private readonly ILogger<RepeatingTaskAPIController> _logger;
        private readonly IRepeatingTaskService repeatService;

        public RepeatingTaskAPIController(ILogger<RepeatingTaskAPIController> logger, IRepeatingTaskService repeatService)
        {
            this.repeatService = repeatService;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/repeatingtask")]
        public ActionResult Index()
        {
            try
            {
                return Ok(repeatService.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RepeatingTaskAPIController.Index");
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("api/repeatingtask/{id}")]
        public ActionResult Details(int id)
        {
            try
            {
                RepeatingTaskModelBL model = repeatService.GetById(id);
                if (model != null)
                    return Ok(model);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RepeatingTaskAPIController.Details");
                return new StatusCodeResult(500);
            }
        }
    }
}

using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskTracker.BL.Interfaces;

namespace TaskTracker.API.Controllers
{
    [ApiController]
    [Authorize(Policy = "ApiScope")]
    public class PriorityAPIController : ControllerBase
    {
        private readonly ILogger<PriorityAPIController> _logger;
        private readonly IPriorityServiceBL priorityService;

        public PriorityAPIController(ILogger<PriorityAPIController> logger, IPriorityServiceBL priorityService)
        {
            this.priorityService = priorityService;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/priority")]
        public ActionResult Index()
        {
            try
            {
                return Ok(priorityService.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PriorityAPIController.Index");
                return new StatusCodeResult(500);
            }
        }
    }
}

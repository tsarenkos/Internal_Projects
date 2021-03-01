using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskTracker.BL.Interfaces;

namespace TaskTracker.API.Controllers
{
    [ApiController]
    [Authorize(Policy = "ApiScope")]
    public class PeriodTypeAPIController : ControllerBase
    {
        private readonly ILogger<PeriodTypeAPIController> _logger;
        private readonly IPeriodTypeService periodService;

        public PeriodTypeAPIController(ILogger<PeriodTypeAPIController> logger, IPeriodTypeService periodService)
        {
            this.periodService = periodService;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/periodtype")]
        public ActionResult Index()
        {
            try
            {
                return Ok(periodService.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PeriodTypeController.Index");
                return new StatusCodeResult(500);
            }
        }
    }
}

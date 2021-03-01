using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskTracker.BL.Interfaces;

namespace TaskTracker.API.Controllers
{
    [ApiController]
    [Authorize(Policy = "ApiScope")]
    public class TaskCategoryAPIController : ControllerBase
    {
        private readonly ILogger<TaskCategoryAPIController> _logger;
        private readonly ICategoryServiceBL categoryService;

        public TaskCategoryAPIController(ILogger<TaskCategoryAPIController> logger, ICategoryServiceBL categoryService)
        {
            this.categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/taskcategory")]
        public ActionResult Index()
        {
            try
            {
                return Ok(categoryService.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TaskCategoryAPIController.Index");
                return new StatusCodeResult(500);
            }
        }
    }
}

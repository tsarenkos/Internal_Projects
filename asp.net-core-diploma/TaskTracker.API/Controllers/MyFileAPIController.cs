using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using TaskTracker.BL.Interfaces;

namespace TaskTracker.API.Controllers
{
    [ApiController]
    [Authorize(Policy = "ApiScope")]
    public class MyFileAPIController : Controller
    {
        private readonly ILogger<MyTaskAPIController> _logger;
        private readonly IFileService _fileService;

        public MyFileAPIController(IFileService fileService, ILogger<MyTaskAPIController> logger)
        {
            _logger = logger;
            _fileService = fileService;
        }

        [HttpGet]
        [Route("api/myfiles/{taskid}/{filename}")]
        public ActionResult Index(int taskid, string filename)
        {
            try
            {
                var model = _fileService.GetByFileName(taskid, filename);
                if (model != null)
                    return Ok(model);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MyFileAPIController.Index");
                return new StatusCodeResult(500);
            }
        }

    }
}

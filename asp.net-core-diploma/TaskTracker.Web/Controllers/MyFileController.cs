using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.Models;
using TaskTracker.Web.Interfaces;
using TaskTracker.Web.Services;

namespace TaskTracker.Web.Controllers
{
    [Authorize(AuthenticationSchemes = "TokenAuthenticationScheme")]
    public class MyFileController : Controller
    {
        private readonly ILogger<MyFileController> _logger;
        private readonly IAPIClient _client;

        public MyFileController(ILogger<MyFileController> logger, IAPIClient client)
        {
            _client = client;
            _logger = logger;
        }

        [HttpGet]
        [Route("files/{taskid}/{filename}")]
        public async Task<FileResult> Index(int taskid, string filename)
        {
            var model = await _client.Get<FileModelBL>($"api/myfiles/{taskid}/{filename}");
            return File(model.Data, model.ContentType);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Logging_App.BLL.Interfaces;
using Logging_App.WEB.Filters;
using System;

namespace Logging_App.WEB.Controllers
{
    public class HomeController : Controller
    {
        private const string Message = "Exception was caught in Controller";
        private readonly ILogger<HomeController> _logger;
        private readonly ILogExceptionService _logExceptionService;        

        public HomeController(ILogExceptionService logExceptionService)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddDebug();
            });

            _logger = loggerFactory.CreateLogger<HomeController>();
            _logExceptionService = logExceptionService;
        }

        [LogExceptionFilter]
        public IActionResult Index()
        {            
            try
            {
                _logExceptionService.LogException();
            }
            catch(Exception exc)
            {
                _logger.LogInformation(Message+"\n"+"\t"+exc.Message);
                throw exc;
            }           
            return View();
        }
    }
}

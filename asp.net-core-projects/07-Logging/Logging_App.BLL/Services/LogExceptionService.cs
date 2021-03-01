using Logging_App.BLL.Interfaces;
using Logging_App.DAL.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace Logging_App.BLL.Services
{
    public class LogExceptionService:ILogExceptionService
    {
        private const string Message = "Exception was caught in BLL";
        private readonly ILogger _logger;
        private readonly IGenerateExcService _generateExcService;

        public LogExceptionService(ILogger<LogExceptionService> logger, IGenerateExcService generateExcService)
        {
            _logger = logger;
            _generateExcService = generateExcService;
        }

        public void LogException()
        {            
            try
            {
                _generateExcService.GenerateExc();
            }
            catch(Exception exc)
            {
                _logger.LogError(Message+"\n"+"\t"+exc.Message);
                throw exc;
            }            
        }
    }
}

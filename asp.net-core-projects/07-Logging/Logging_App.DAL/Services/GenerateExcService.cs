using Logging_App.DAL.Interfaces;
using Microsoft.Extensions.Logging;
using System;


namespace Logging_App.DAL.Services
{
    public class GenerateExcService:IGenerateExcService
    {
        private const string Message = "Exception was generated in DAL";
        private readonly ILogger _logger;

        public GenerateExcService(ILogger<GenerateExcService> logger)
        {
            _logger = logger;
        }

        public void GenerateExc()
        {
            _logger.LogError(Message);

            throw new Exception(Message);
        }
    }
}

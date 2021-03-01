using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using WebApiApp.BLL.Interfaces;

namespace WebApiApp.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {       
        private const string pathDir = @"F:\CORE_STUDY\Web API\WebApiApp\WebApiApp.WEB\bin";
        private IFileService fileService;

        public FileController(IFileService fileService)
        {
            this.fileService = fileService;
        }    
        
        [HttpGet]
        public IStatusCodeActionResult GetFiles()
        {
            List<string> files = fileService.GetFilesList(pathDir);

            if (files == null)
            {
                return NotFound();
            }

            return new OkObjectResult(files);
        }

        [HttpGet("{page}")]
        public IStatusCodeActionResult GetFilesOnPage(int page)
        {
            if (page <= 0)
            {
                return BadRequest();
            }

            List<string> files = fileService.GetFilesOnPage(pathDir, page);

            if(files == null)
            {
                return NotFound();
            }

            return new OkObjectResult(files);
        }
    }
}

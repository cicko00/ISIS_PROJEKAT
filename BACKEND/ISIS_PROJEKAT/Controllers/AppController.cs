using ISIS_PROJEKAT.Repository;
using ISIS_PROJEKAT.Service;
using Microsoft.AspNetCore.Mvc;

namespace ISIS_PROJEKAT.Controllers
{
    public class AppController:ControllerBase
    {
        IAppService _appService;
        public AppController(IAppService appService)
        { 
            _appService = appService;
        }



        [HttpPost("UploadData")]
        public IActionResult UploadData( [FromForm]IFormFile[] CsvFile)
        {
            _appService.ReciveData(CsvFile);
            return Ok();
        }

        [HttpGet("GetResult")]
        public IActionResult GetResult([FromQuery]int NoOfDays)
        {
            return Ok(_appService.GetResult(NoOfDays));
        }
    }
}

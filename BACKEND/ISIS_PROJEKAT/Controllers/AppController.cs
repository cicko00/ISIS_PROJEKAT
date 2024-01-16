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

        [HttpGet("TrainWithData")]
        public IActionResult TrainWithData([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {

            return Ok(_appService.TrainWithData(startDate, endDate));
        }
        


        [HttpGet("GetResult")]
        public IActionResult GetResult([FromQuery]int NoOfDays, [FromQuery] DateTime StartDate)
        {
            return Ok(_appService.GetResult(NoOfDays,StartDate));
        }
    }
}

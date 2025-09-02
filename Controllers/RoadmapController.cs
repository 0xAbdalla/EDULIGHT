using EDULIGHT.Services.AppService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EDULIGHT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoadmapController : ControllerBase
    {
        private readonly IAppService _appService;

        public RoadmapController(IAppService appService)
        {
            _appService = appService;
        }


        [HttpGet("GetAllRoadmaps")]
        public async Task<IActionResult> GetAllRoadmaps()
        {
            var result = await _appService.GetAllRoadmapsAsync();
            return Ok(result);
        }
    }
}

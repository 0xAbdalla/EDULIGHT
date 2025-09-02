using EDULIGHT.Dto.Category;
using EDULIGHT.Services.AppService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EDULIGHT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IAppService _appService;
        private new List<String> AllowedExtension = new List<string> { ".jpg", ".png" };
        private long AllowedSize = 10485760;

        public CategoryController(IAppService appService)
        {
            _appService = appService;
        }


        [HttpGet(Name = "GetAllCategoriesAsync")]
        public async Task<IActionResult> GetAllCategoriesAsync() 
        {
            var result = await _appService.GetAllCategoriesAsync();
            return Ok(result);
        }
        [HttpPost(Name = "AddCategoryAsync")]
        public async Task<IActionResult> AddCategoryAsync([FromForm] PostCategoryDto dto)
        {
            if (!AllowedExtension.Contains(Path.GetExtension(dto.Poster.FileName).ToLower())) 
            {
                return BadRequest("The poster extension should be .jpg and .png !!");
            }
            if (dto.Poster.Length > AllowedSize)
            {
                return BadRequest("The Size should be only 10Miga !!");
            }
            var result = await _appService.AddCategoryAsync(dto);
            return Ok(result);
        }

    }
}

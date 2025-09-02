using System.ComponentModel.DataAnnotations;

namespace EDULIGHT.Dto.Category
{
    public class PostCategoryDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public IFormFile Poster { get; set; }

    }
}

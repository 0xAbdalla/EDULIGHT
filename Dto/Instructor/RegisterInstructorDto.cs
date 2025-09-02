using System.ComponentModel.DataAnnotations;

namespace EDULIGHT.Dto.Instructor
{
    public class RegisterInstructorDto
    {
        [Required, StringLength(255)]
        public string FullName { get; set; }
        [Required, StringLength(100),EmailAddress]
        public string Email { get; set; }
        [Required]
        public int ExperienceYears { get; set; }
        [Required, MaxLength(500)]
        public string BioExpertise { get; set; }
        [Required, StringLength(30)]
        public string Password { get; set; }
    }
}

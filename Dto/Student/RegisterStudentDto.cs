using System.ComponentModel.DataAnnotations;

namespace EDULIGHT.Dto.Student
{
    public class RegisterStudentDto
    {
        [Required, StringLength(255)]
        public string FullName { get; set; }
        [Required, StringLength(100),EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(30)]
        public string Password { get; set; }
    }
}

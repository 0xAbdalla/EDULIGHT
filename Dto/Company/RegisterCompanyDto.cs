using System.ComponentModel.DataAnnotations;

namespace EDULIGHT.Dto.Company
{
    public class RegisterCompanyDto
    {
        [Required, StringLength(255)]
        public string FullName { get; set; }
        [Required, StringLength(100),EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(100)]
        public string Industry { get; set; }
        [Required, StringLength(255)]
        public string AddressofCompany { get; set; }
        [Required, StringLength(255)]
        public string Link { get; set; }
        [Required, StringLength(30)]
        public string Password { get; set; }
    }
}

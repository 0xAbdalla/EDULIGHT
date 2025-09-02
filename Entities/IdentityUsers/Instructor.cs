using EDULIGHT.Entities.IdentityUsers;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDULIGHT.Entities.Users
{
    public class Instructor : AppUser
    {
        [NotMapped]
        public IFormFile image { get; set; }
        public string imageURL { get; set; }
        public int ExperienceYears { get; set; }
        [Required,MaxLength(500)]
        public string BioExpertise { get; set; }
    }
}

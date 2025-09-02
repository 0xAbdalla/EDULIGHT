using EDULIGHT.Entities.IdentityUsers;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EDULIGHT.Entities.Users
{
    public class Companies : AppUser
    {
        public string Industry { get; set; }
        public string AddressofCompany { get; set; }
        public string Link { get; set; }


    }
}

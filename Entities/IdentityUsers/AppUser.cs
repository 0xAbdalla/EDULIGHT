using Microsoft.AspNetCore.Identity;

namespace EDULIGHT.Entities.IdentityUsers
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime Created_at => DateTime.Now;

    }
}

using EDULIGHT.Entities.IdentityUsers;
using EDULIGHT.Entities.Users;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EDULIGHT.Services.AuthService.TokenService
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> CreateTokenStudentAsync(Student student, UserManager<AppUser> userManager);
        Task<JwtSecurityToken> CreateTokenInstructorAsync(Instructor instructor, UserManager<AppUser> userManager);
        Task<JwtSecurityToken> CreateTokenCompanyAsync(Companies Company, UserManager<AppUser> userManager);
    }
}

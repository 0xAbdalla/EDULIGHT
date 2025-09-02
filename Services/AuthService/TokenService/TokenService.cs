using EDULIGHT.Entities.IdentityUsers;
using EDULIGHT.Entities.Users;
using EDULIGHT.Identity.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EDULIGHT.Services.AuthService.TokenService
{
    public class TokenService : ITokenService 
    {
        private readonly MapJWT jwt;

        public TokenService(IOptions<MapJWT> jwt)
        {
            this.jwt = jwt.Value;
        }
        public async Task<JwtSecurityToken> CreateTokenStudentAsync(Student student, UserManager<AppUser> userManager)
        {
            var userClaims = await userManager.GetClaimsAsync(student);
            var roles = await userManager.GetRolesAsync(student);
            var roleClaims = new List<Claim>();
            foreach (var role in roles) 
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email,student.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub,student.FullName),
                new Claim("uid",student.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signinCredintial = new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer:jwt.Issuer,
                audience:jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(jwt.DurationInDays),
                signingCredentials:signinCredintial);

            return token;
            
        }

        public async Task<JwtSecurityToken> CreateTokenCompanyAsync(Companies Company, UserManager<AppUser> userManager)
        {
            var userClaims = await userManager.GetClaimsAsync(Company);
            var roles = await userManager.GetRolesAsync(Company);
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email,Company.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub,Company.FullName),
                new Claim("uid",Company.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signinCredintial = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(jwt.DurationInDays),
                signingCredentials: signinCredintial);

            return token;
        }

        public async Task<JwtSecurityToken> CreateTokenInstructorAsync(Instructor instructor, UserManager<AppUser> userManager)
        {
            var userClaims = await userManager.GetClaimsAsync(instructor);
            var roles = await userManager.GetRolesAsync(instructor);
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email,instructor.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub,instructor.FullName),
                new Claim("uid",instructor.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signinCredintial = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(jwt.DurationInDays),
                signingCredentials: signinCredintial);

            return token;
        }

    }
}

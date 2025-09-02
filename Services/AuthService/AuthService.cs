using EDULIGHT.Dto.Company;
using EDULIGHT.Dto.Instructor;
using EDULIGHT.Dto.Login;
using EDULIGHT.Dto.ResetPassword;
using EDULIGHT.Dto.Student;
using EDULIGHT.Entities.IdentityUsers;
using EDULIGHT.Entities.Users;
using EDULIGHT.Identity.Context;
using EDULIGHT.Services.AuthService.TokenService;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Common;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Humanizer;
using Microsoft.CodeAnalysis.Emit;

namespace EDULIGHT.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly ITokenService tokenService;

        public AuthService(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
        }
        public async Task<AuthModel> RegisterStudent(RegisterStudentDto student)
        {
            if (await userManager.FindByEmailAsync(student.Email) is not null)
                return new AuthModel { Message = "Email is already registered!." };
            var userstudent = new Student
            {
                FullName = student.FullName,
                Email = student.Email,
                UserName = student.Email.Split("@")[0]
            };
            var result = await userManager.CreateAsync(userstudent, student.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description} ";
                }
                return new AuthModel { Message = errors };
            }
            await userManager.AddToRoleAsync(userstudent, "Student");
            var token = await tokenService.CreateTokenStudentAsync(userstudent, userManager);
            var roles = await userManager.GetRolesAsync(userstudent);
            return new AuthModel
            {
                IsAuthenticated = true,
                Username = userstudent.UserName,
                Email = userstudent.Email,
                Role = roles.FirstOrDefault(),
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireOn = token.ValidTo

            };
        }
        public async Task<AuthModel> RegisterCompany(RegisterCompanyDto company)
        {

            if (await userManager.FindByEmailAsync(company.Email) is not null)
                return new AuthModel { Message = "Email is already registered!." };
            var usercompany = new Companies
            {
                FullName = company.FullName,
                Industry = company.Industry,
                AddressofCompany = company.AddressofCompany,
                Link = company.Link,
                Email = company.Email,
                UserName = company.Email.Split("@")[0]

            };
            var result = await userManager.CreateAsync(usercompany, company.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description} ";
                }
                return new AuthModel { Message = errors };
            }
            await userManager.AddToRoleAsync(usercompany, "Company");
            var token = await tokenService.CreateTokenCompanyAsync(usercompany, userManager);
            var roles = await userManager.GetRolesAsync(usercompany);
            return new AuthModel
            {
                IsAuthenticated = true,
                Username = usercompany.UserName,
                Email = usercompany.Email,
                Role = roles.FirstOrDefault(),
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireOn = token.ValidTo

            };
        }
        public async Task<AuthModel> RegisterInstructor(RegisterInstructorDto instructor)
        {


            if (await userManager.FindByEmailAsync(instructor.Email) is not null)
                return new AuthModel { Message = "Email is already registered!." };
            var userinstructor = new Instructor
            {
                FullName = instructor.FullName,
                BioExpertise = instructor.BioExpertise,
                ExperienceYears = instructor.ExperienceYears,
                Email = instructor.Email,
                UserName = instructor.Email.Split("@")[0]

            };
            var result = await userManager.CreateAsync(userinstructor, instructor.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description} ";
                }
                return new AuthModel { Message = errors };
            }
            await userManager.AddToRoleAsync(userinstructor, "Instructor");
            var token = await tokenService.CreateTokenInstructorAsync(userinstructor, userManager);
            var roles = await userManager.GetRolesAsync(userinstructor);
            return new AuthModel
            {
                IsAuthenticated = true,
                Username = userinstructor.UserName,
                Email = userinstructor.Email,
                Role = roles.FirstOrDefault(),
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireOn = token.ValidTo
            };
        }

        //__________________________________________________________________________________________
        public async Task<AuthModel> LoginStudent(LoginDto student)
        {
            var authmodel = new AuthModel();
            var userstudent = await userManager.FindByEmailAsync(student.Email);
            if (userstudent == null 
                || !await userManager.CheckPasswordAsync(userstudent,student.Password) 
                || !await userManager.IsInRoleAsync(userstudent, "Student")) 
            {
                authmodel.Message = "Email or password is incorrect!!";
                return authmodel;
            }
            var token = await tokenService.CreateTokenStudentAsync((Student)userstudent,userManager);
            var getrole = await userManager.GetRolesAsync(userstudent);
            authmodel.IsAuthenticated = true;
            authmodel.Username = userstudent.UserName;
            authmodel.Email = userstudent.Email;
            authmodel.Role = getrole.FirstOrDefault();
            authmodel.Token = new JwtSecurityTokenHandler().WriteToken(token);
            authmodel.ExpireOn = token.ValidTo;
            return authmodel;
        }
        public async Task<AuthModel> LoginInstructor(LoginDto instructor)
        {
            var authmodel = new AuthModel();
            var useristructor = await userManager.FindByEmailAsync(instructor.Email);
            if (useristructor == null || !await userManager.CheckPasswordAsync(useristructor, instructor.Password) || !await userManager.IsInRoleAsync(useristructor, "Instructor"))
            {
                authmodel.Message = "Email or password is incorrect!!";
                return authmodel;
            }
            var token = await tokenService.CreateTokenInstructorAsync((Instructor)useristructor, userManager);
            var getrole = await userManager.GetRolesAsync(useristructor);
            authmodel.IsAuthenticated = true;
            authmodel.Username = useristructor.UserName;
            authmodel.Email = useristructor.Email;
            authmodel.Role = getrole.FirstOrDefault();
            authmodel.Token = new JwtSecurityTokenHandler().WriteToken(token);
            authmodel.ExpireOn = token.ValidTo;
            return authmodel;
        }
        public async Task<AuthModel> LoginCompany(LoginDto company)
        {

            var authmodel = new AuthModel();
            var usercompany = await userManager.FindByEmailAsync(company.Email);
            if (usercompany == null || !await userManager.CheckPasswordAsync(usercompany, company.Password) || !await userManager.IsInRoleAsync(usercompany, "Company"))
            {
                authmodel.Message = "Email or password is incorrect!!";
                return authmodel;
            }
            var token = await tokenService.CreateTokenCompanyAsync((Companies)usercompany, userManager);
            var getrole = await userManager.GetRolesAsync(usercompany);
            authmodel.IsAuthenticated = true;
            authmodel.Username = usercompany.UserName;
            authmodel.Email = usercompany.Email;
            authmodel.Role = getrole.FirstOrDefault();
            authmodel.Token = new JwtSecurityTokenHandler().WriteToken(token);
            authmodel.ExpireOn = token.ValidTo;
            return authmodel;
        }
        //Store OTP -->
        private static Dictionary<string, string> OTPstore = new Dictionary<string, string>();
        public void storeOTP(string Email, string OTP)
        {
            OTPstore[Email] = OTP;
        }

        public string getOTP(string Email)
        {
            return OTPstore[Email];
        }

        // Send the OTP to the Email -->
        public async Task sendOTPEmail(string email, string otp)
        {
            var emailService = new EmailService("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");
            var subject = "Password Reset OTP";
            var body = $"Your OTP for password reset is: {otp}";

            await emailService.SendEmail(email, subject, body);

        }
        public async Task sendOTPEmailConfirmation(string email, string otp)
        {
            var emailService = new EmailService("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");
            var subject = "Change Email OTP";
            var body = $"Your OTP for Change Email is: {otp}";

            await emailService.SendEmail(email, subject, body);

        }
        public async Task<bool> ResetPassword(string token,string NewPassword,string email)
        {
            var getemail = await userManager.FindByEmailAsync(email);
            if (getemail == null) return false;
            var resetpass = await userManager.ResetPasswordAsync(getemail,token,NewPassword);
            if (resetpass == null) return false;
            return true;
        }

        public async Task<AuthModel> ChangeEmail(string CurrentEmail, string NewEmail)
        {
            var getuser = await userManager.FindByEmailAsync(CurrentEmail);
            var gentoken = await userManager.GenerateChangeEmailTokenAsync(getuser, NewEmail);
            var changeemail = await userManager.ChangeEmailAsync(getuser, NewEmail, gentoken);
            return new AuthModel { Email = getuser.Email};
        }

        public async Task<bool> ChangeName(string username,string NewName)
        {
            var getstudent = await userManager.FindByNameAsync(username);
            if (getstudent == null) return false;
            getstudent.FullName = NewName;
            var applychange = await userManager.UpdateAsync(getstudent);
            if (applychange == null) return false;
            return true;
        }
    }
}

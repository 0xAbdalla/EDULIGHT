using EDULIGHT.Dto.Company;
using EDULIGHT.Dto.Instructor;
using EDULIGHT.Dto.Login;
using EDULIGHT.Dto.ResetPassword;
using EDULIGHT.Dto.Student;
using EDULIGHT.Identity.Context;
using System.IdentityModel.Tokens.Jwt;

namespace EDULIGHT.Services.AuthService
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterStudent(RegisterStudentDto student);
        Task<AuthModel> RegisterInstructor(RegisterInstructorDto instructor);
        Task<AuthModel> RegisterCompany(RegisterCompanyDto company);
        Task<AuthModel> LoginStudent(LoginDto student);
        Task<AuthModel> LoginInstructor(LoginDto instructor);
        Task<AuthModel> LoginCompany(LoginDto company);
        void storeOTP(string Email, string OTP);
        string getOTP(string Email);
        Task sendOTPEmail(string email, string otp);
        Task sendOTPEmailConfirmation(string email, string otp);
        Task<bool> ResetPassword(string token, string NewPassword, string email);
        Task<AuthModel> ChangeEmail(string CurrentEmail, string NewEmail);
        Task<bool> ChangeName(string name,string NewName);


    }
}

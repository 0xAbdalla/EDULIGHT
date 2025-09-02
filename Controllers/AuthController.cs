using EDULIGHT.Dto.Company;
using EDULIGHT.Dto.Instructor;
using EDULIGHT.Dto.Login;
using EDULIGHT.Dto.ResetPassword;
using EDULIGHT.Dto.Student;
using EDULIGHT.Entities.IdentityUsers;
using EDULIGHT.Errors;
using EDULIGHT.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.VisualBasic;
using System.Security.Claims;

namespace EDULIGHT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly UserManager<AppUser> _userManager;

        public AuthController(IAuthService authService, UserManager<AppUser> userManager)
        {
            this.authService = authService;
            _userManager = userManager;
        }
        [HttpPost("LoginStudent")]
        public async Task<IActionResult> LoginStudent([FromBody] LoginDto login)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            var result = await authService.LoginStudent(login);
            if (!result.IsAuthenticated) 
            {
                return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            }
            return Ok(result);
        }
        [HttpPost("RegisterStudent")]
        public async Task<IActionResult> RegisterStudent([FromBody] RegisterStudentDto register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await authService.RegisterStudent(register);
            if (!result.IsAuthenticated)
            {
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest,"Invalid Registration!!"));
            }
            return Ok(result);
        }
        [HttpPost("LoginInstructor")]
        public async Task<IActionResult> LoginInstructor([FromBody] LoginDto login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await authService.LoginInstructor(login);
            if (!result.IsAuthenticated)
            {
                return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            }
            return Ok(result);
        }
        [HttpPost("RegisterInstructor")]
        public async Task<IActionResult> RegisterInstructor([FromBody] RegisterInstructorDto register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await authService.RegisterInstructor(register);
            if (!result.IsAuthenticated)
            {
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Invalid Registration!!"));
            }
            return Ok(result);
        }
        [HttpPost("LoginCompany")]
        public async Task<IActionResult> LoginCompany([FromBody] LoginDto login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await authService.LoginCompany(login);
            if (!result.IsAuthenticated)
            {
                return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            }
            return Ok(result);
        }
        [HttpPost("RegisterCompany")]
        public async Task<IActionResult> RegisterCompany([FromBody] RegisterCompanyDto register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await authService.RegisterCompany(register);
            if (!result.IsAuthenticated)
            {
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Invalid Registration!!"));
            }
            return Ok(result);
        }
        [HttpPost("ForgetPassword")]
        public async Task<ActionResult> ForgetPasswrod(string Email)
        {
            if (Email == null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest,"The input is null !"));
            var getemail = await _userManager.FindByEmailAsync(Email);
            if (getemail == null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Not Found Email !"));
            var OTP = new Random().Next(1000, 9999).ToString();
            authService.storeOTP(Email, OTP);
            await authService.sendOTPEmail(Email, OTP);
            return Ok("The OTP sended to the Email.");        
        }

        [HttpPost("VerifyOtp")]
        public ActionResult VerifyOtp(string email, string otp)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(otp)) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Email and OTP are required."));
            var storedOtp = authService.getOTP(email);
            if (storedOtp == null || storedOtp != otp) return NotFound(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Invalid OTP."));
            return Ok("OTP verified successfully.");
        }
        // To Update Password --> 
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string email, string NewPassword)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(NewPassword)) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Email and new password are required."));
            var getuser = await _userManager.FindByEmailAsync(email);
            if (getuser == null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Not Found the Email."));
            var gentoken = await _userManager.GeneratePasswordResetTokenAsync(getuser);
            var passwordUpdated = await authService.ResetPassword(gentoken,NewPassword,email);
            if(passwordUpdated == false) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Failed to update password."));
            return Ok("Password updated successfully.");
        }
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string CurrentPassword, string NewPassword, string NewConrirmPassword)
        {
            var user = User.FindFirst(ClaimTypes.Email).Value;
            if (string.IsNullOrEmpty(CurrentPassword) || string.IsNullOrEmpty(NewPassword)) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "current  and new password are required."));
            var getuser = await _userManager.FindByEmailAsync(user);
            if (getuser == null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Not Found the Email."));
            var gentoken = await _userManager.GeneratePasswordResetTokenAsync(getuser);
            var passwordUpdated = await authService.ResetPassword(gentoken, NewPassword, user);
            if (passwordUpdated == false) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Failed to Change password."));
            return Ok("Password Change successfully.");
        }
        [HttpPost("ChangeEmail")]
        public async Task<ActionResult> ChangeEmail(string CurrentEmail, string NewEmail, string NewConrirmEmail)
        {
            if (CurrentEmail == null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "The input is null !"));
            var getemail = await _userManager.FindByEmailAsync(CurrentEmail);
            if (getemail == null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Not Found Email !"));
            var OTP = new Random().Next(1000, 9999).ToString();
            authService.storeOTP(NewEmail, OTP);
            await authService.sendOTPEmailConfirmation(NewEmail, OTP);
            var change = await authService.ChangeEmail(CurrentEmail, NewEmail);
            return Ok("The OTP sended to the New Email.");
        }
        [HttpPost("VerifyOtpChangeEmail")]
        public ActionResult VerifyOtpChangeEmail(string NewEmail, string otp)
        {
            if (string.IsNullOrEmpty(NewEmail) || string.IsNullOrEmpty(otp)) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Email and OTP are required."));
            var storedOtp = authService.getOTP(NewEmail);
            if (storedOtp == null || storedOtp != otp) return NotFound(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Invalid OTP."));
            return Ok("OTP verified successfully and Change Email are successfully.");
        }
        [HttpPost("ChangeName")]
        public async Task<ActionResult> ChangeName(string NewName)
        {
            var user = User.FindFirst(ClaimTypes.Email).Value;
            var getuser = await _userManager.FindByEmailAsync(user);    

            if (user == null) return NotFound("Not Found User.");
            if(string.IsNullOrEmpty(NewName))
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "New Name are required."));
            var changename = await authService.ChangeName(getuser.UserName, NewName);
            if (changename == false) return BadRequest("Failed to Change Name");
            return Ok("Success To Change Name.");
        }


    }
}

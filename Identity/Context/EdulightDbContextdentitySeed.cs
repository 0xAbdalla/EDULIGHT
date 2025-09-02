using EDULIGHT.Entities.IdentityUsers;
using EDULIGHT.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EDULIGHT.Identity.Context
{
    public static class EdulightDbContextdentitySeed
    {
        public async static Task SeedUsersAsunc(UserManager<AppUser> userManager)
        {
            if (userManager.Users.Count() == 0)
            {
                var student = new Student()
                {
                    FullName = "Abdullah Elseady",
                    Email = "abdullahelseady12@gmail.com",
                    UserName = "abdullahelseady12"
                };
                await userManager.CreateAsync(student, "Abdullah90#");
                await userManager.AddToRoleAsync(student, "Student");
                var instructor = new Instructor()
                {
                    FullName = "Omar Elsharkawy",
                    Email = "omarelsharkawy12@gmail.com",
                    ExperienceYears = 5,
                    BioExpertise = "I am Omar Elsharkawy results-driven junior .NET developer with a strong foundation in web application development and proficiency in the .NET stack. Experienced in backend development through an internship focused on creating applications for educational institutions.",
                    UserName = "omarelsharkawy12"

                };
                await userManager.CreateAsync(instructor, "Ommaarrrr90#");
                await userManager.AddToRoleAsync(instructor, "Instructor");

                var company = new Companies()
                {
                    FullName = "JebGuard",
                    Email = "jebguard12@gmail.com",
                    Industry = "Threat Hunting",
                    AddressofCompany = "Egypt - Cairo",
                    Link = "https://www.figma.com/design/",
                    UserName = "jebguard12"
                };
                await userManager.CreateAsync(company, "JebGuard90#");
                await userManager.AddToRoleAsync(company, "Company");
            }
        }
    }
}

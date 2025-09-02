using EDULIGHT.Configrations;
using EDULIGHT.ContentDataSeed;
using EDULIGHT.Entities.IdentityUsers;
using EDULIGHT.Entities.Users;
using EDULIGHT.Identity.Context;
using EDULIGHT.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace EDULIGHT.Helper
{
    public static class ConfigureMiddleware
    {
        public static async Task<WebApplication> ConfigureMiddlewareAsync(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<EdulightDbContext>();
            var identitycontext = services.GetRequiredService<EdulightIdentityDbContext>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            //var instructorManager = services.GetRequiredService<UserManager<Instructor>>();
            //var companyManager = services.GetRequiredService<UserManager<Companies>>();
            var loggerfactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await context.Database.MigrateAsync();
                await identitycontext.Database.MigrateAsync();
                await EdulightDbContextdentitySeed.SeedUsersAsunc(userManager);
                await EdulightDbContextSeed.SeedAsync(context);
            }
            catch (Exception ex)
            {
                var logger = loggerfactory.CreateLogger<Program>();
                logger.LogError(ex, "This database already updated...");
            }


            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference(options =>
                {
                    options.Title = "My Project ";
                    options.Theme = ScalarTheme.BluePlanet;
                    options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
                    options.CustomCss = "";
                    options.ShowSidebar = true;
                    options.Authentication = new ScalarAuthenticationOptions
                    {
                        PreferredSecurityScheme = "Bearer"
                    };
                });
                //app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json","v1"));

            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            return app;
        }
    }
}

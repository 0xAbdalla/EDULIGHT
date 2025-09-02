using EDULIGHT.Configrations;
using EDULIGHT.Entities.IdentityUsers;
using EDULIGHT.Entities.Users;
using EDULIGHT.Errors;
using EDULIGHT.Identity.Context;
using EDULIGHT.Repositories;
using EDULIGHT.Services.AppService;
using EDULIGHT.Services.AuthService;
using EDULIGHT.Services.AuthService.TokenService;
using EDULIGHT.Services.CartService;
using EDULIGHT.Services.CourseToCart;
using EDULIGHT.Services.EnrollmentService;
using EDULIGHT.Services.OrderService;
using EDULIGHT.Services.PaymentService;
using EDULIGHT.Services.ReviewService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Collections;
using System.Text;

namespace EDULIGHT.Helper
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependency(this IServiceCollection services,IConfiguration configuration) 
        {

            services.AddBuiltInService();
            services.AddScalarService();
            services.AddDbContextService(configuration);
            services.AddRepositoryRigsterService();
            services.AddServices();
            services.AddAutoMapperService();
            services.ConfigureInvalidModelStateService();
            services.AddIdentityService();
            services.JWTService(configuration);
            services.AddAuthenticationService(configuration);
            services.AddRedisService(configuration);
            services.CrossAllowOrigin();
            services.ConfigureLimitServices();

            return services;
        }
        private static IServiceCollection ConfigureLimitServices(this IServiceCollection services)
        {
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 100_000_000; // Set to 100 MB or desired limit
            });
            return services;
        }

        private static IServiceCollection CrossAllowOrigin(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin()
                                   .AllowAnyMethod()
                                   .AllowAnyHeader());
            });
            return services;
        }
        private static IServiceCollection AddBuiltInService(this IServiceCollection services) 
        {
            services.AddControllers();
            return services;
        }
        private static IServiceCollection AddScalarService(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddOpenApi();
            return services;
        }
        private static IServiceCollection AddDbContextService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<EdulightDbContext>(op =>
            {
                op.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),p=>p.MigrationsAssembly(typeof(EdulightDbContext).Assembly.FullName));
            });
            services.AddDbContext<EdulightIdentityDbContext>(op =>
            {
                op.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });
            return services;
        }
        private static IServiceCollection AddRepositoryRigsterService(this IServiceCollection services)
        {
            services.AddScoped<IAppService, AppService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddSingleton<Hashtable>();
            return services;
        }
        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IPaymentService,PaymentService>();
            services.AddScoped<ICourseToCart, CourseToCart>();
            services.AddScoped<ICartService,CartService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IAppService, AppService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
        private static IServiceCollection AddAutoMapperService(this IServiceCollection services)
        {
            services.AddAutoMapper(m => m.AddProfile(new EDULIGHT.Mapping.AppProfile()));

            return services;
        }
        private static IServiceCollection ConfigureInvalidModelStateService(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                    .SelectMany(p => p.Value.Errors)
                    .Select(p => p.ErrorMessage)
                    .ToList();
                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            });
            return services;
        }
        private static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromHours(10));
            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.SignIn.RequireConfirmedEmail = true;
                opt.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            }).AddEntityFrameworkStores<EdulightIdentityDbContext>().AddDefaultTokenProviders();
            return services;
        }
        private static IServiceCollection JWTService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MapJWT>(configuration.GetSection("JWT"));
            return services;
        }
        private static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))

                    };
                });
            return services;
        }
        private static IServiceCollection AddRedisService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                var connect = configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connect);
            });

            return services;
        }




    }
}

using EDULIGHT.Configrations;
using EDULIGHT.Errors;
using EDULIGHT.Helper;
using EDULIGHT.Middlewares;
using EDULIGHT.Repositories;
using EDULIGHT.Services.AppService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Scalar.AspNetCore;
using System.Collections;
using System.Configuration;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDependency(builder.Configuration);

var app = builder.Build();
await app.ConfigureMiddlewareAsync();
app.Run();



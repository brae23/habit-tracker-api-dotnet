using FluentValidation;
using HabitTracker.Api.Infrastructure;
using HabitTracker.Api.Infrastructure.Entities;
using HabitTracker.Api.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHealthChecks();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "Habit Tracker API",
            Version = "v1",
            Description = "API for Habit Tracker application"
        });
        options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = JwtBearerDefaults.AuthenticationScheme
        });

        options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
    });

builder.Services.AddDbContext<HabitTrackerDbContext>();
builder.Services.AddDistributedMemoryCache();

// Auth
builder.Services.AddAuthorization();
builder.Services.AddIdentityCore<User>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.Tokens.AuthenticatorIssuer = "HabitTracker.Api";
    options.Tokens.AuthenticatorTokenProvider = "HabitTracker.Api";
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = true;
})
    .AddEntityFrameworkStores<HabitTrackerDbContext>()
    .AddApiEndpoints()
    .AddDefaultTokenProviders();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
        options.DefaultAuthenticateScheme = IdentityConstants.BearerScheme;
        options.DefaultChallengeScheme = IdentityConstants.BearerScheme;
    })
    .AddCookie(IdentityConstants.BearerScheme, options =>
    {
        options.Cookie.Name = "HabitTracker.Application";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.ReturnUrlParameter = "account";
        options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
        {
            OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return System.Threading.Tasks.Task.CompletedTask;
            },
            OnRedirectToAccessDenied = context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return System.Threading.Tasks.Task.CompletedTask;
            }
        };
    });

// Dependency Injection

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<HabitTrackerDbContext>();
        dbContext.Database.Migrate();
    }
}

app.UseHttpsRedirection();

app.MapIdentityApi<User>();

app.UseExceptionHandler("/error");

app.UseHealthChecks("/healthCheck");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors(x => {
    string[] origins = ["http://localhost:8100"];
    x.WithOrigins(origins);
    x.AllowAnyMethod();
    x.AllowCredentials();
    x.AllowAnyHeader();
    });


app.Run();

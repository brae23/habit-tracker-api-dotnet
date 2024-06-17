using FluentValidation;
using HabitTracker.Api.Infrastructure;
using HabitTracker.Api.Infrastructure.Entities;
using HabitTracker.Api.Requests.Auth;
using HabitTracker.Api.Services.Auth;
using HabitTracker.Api.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<HabitTrackerDbContext>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(5);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Dependency Injection
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

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

app.UseSession();

app.UseExceptionHandler("/error");

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

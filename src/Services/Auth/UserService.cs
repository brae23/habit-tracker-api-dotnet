using System.Net;
using HabitTracker.Api.Infrastructure;
using HabitTracker.Api.Infrastructure.Entities;
using HabitTracker.Api.Models;
using HabitTracker.Api.Requests.Auth;
using HabitTracker.Api.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Api.Services.Auth;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly HabitTrackerDbContext _dbContext;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(ILogger<UserService> logger, HabitTrackerDbContext dbContext, IPasswordHasher<User> passwordHasher)
    {
        _logger = logger;
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserCRUDResult> LoginUserAsync(LoginRequest request)
    {
        try
        {
            var userEntity = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == request.Username);

            if (userEntity == null)
            {
                _logger.LogInformation($"User with UserName <{request.Username}> does not exist. Returning not found result.");
                return new UserCRUDResult(HttpStatusCode.NotFound);
            }

            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(userEntity, userEntity.PasswordHash, request.Password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                _logger.LogInformation($"Supplied password for UserName <{request.Username}> does not match. Returning bad request result.");
                return new UserCRUDResult(HttpStatusCode.BadRequest);
            }

            if (passwordVerificationResult == PasswordVerificationResult.SuccessRehashNeeded)
            {
                _logger.LogInformation($"Supplied password for UserName <{request.Username}> uses a deprecated hashing algorithm. A new password must be set!");
                return new UserCRUDResult(HttpStatusCode.SeeOther, new UserDTO(userEntity));
            }

            return new UserCRUDResult(HttpStatusCode.OK, new UserDTO(userEntity));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Encountered exception while logging in user with UserName <{request.Username}>. Returning internal server error result. Exception: {ex}");
            return new UserCRUDResult(HttpStatusCode.InternalServerError);
        }
    }

    public async Task<UserCRUDResult> CreateUserAsync(CreateUserRequest request)
    {
        try
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == request.Username);

            if (user != null)
            {
                _logger.LogInformation($"User with UserName <{request.Username}> already exists. Not creating new user");
                return new UserCRUDResult(HttpStatusCode.Conflict, new UserDTO(user));
            }

            var userEntity = new User
            {
                UserId = Guid.NewGuid(),
                UserName = request.Username,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = string.Empty,
                AccountCreatedDate = DateTime.UtcNow
            };

            var passwordHash = _passwordHasher.HashPassword(userEntity, request.Password);
            userEntity.PasswordHash = passwordHash;

            _dbContext.Users.Add(userEntity);
            await _dbContext.SaveChangesAsync();
            return new UserCRUDResult(HttpStatusCode.Created, new UserDTO(userEntity));
        }
        catch(Exception ex)
        {
            _logger.LogError($"Encountered exception while creating user with UserName <{request.Username}>. Returning internal server error result. Exception: {ex}");
            return new UserCRUDResult(HttpStatusCode.InternalServerError);
        }
    }
}
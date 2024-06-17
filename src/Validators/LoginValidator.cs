using FluentValidation;
using HabitTracker.Api.Requests.Auth;

namespace HabitTracker.Api.Validators;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Username).NotEmpty().Length(3, 128);
        RuleFor(x => x.Password).NotEmpty().Length(12, 128);
    }
}
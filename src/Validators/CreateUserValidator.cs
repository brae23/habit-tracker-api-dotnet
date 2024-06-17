using FluentValidation;
using HabitTracker.Api.Requests.Auth;

namespace HabitTracker.Api.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Username).NotEmpty().Length(3, 128);
        RuleFor(x => x.Password).NotEmpty().Length(12, 128);
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
    }
}
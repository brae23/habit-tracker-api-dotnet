using FluentValidation;
using HabitTracker.Api.Requests;

namespace HabitTracker.Api.Validators;

public class CreateListRequestValidator : AbstractValidator<CreateListRequest>
{
    public CreateListRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("List name is required.")
            .MaximumLength(100).WithMessage("List name must be 100 characters or fewer.");
        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("List type is required and must be a valid enum value.");
    }
}

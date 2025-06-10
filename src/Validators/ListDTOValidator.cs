using FluentValidation;
using HabitTracker.Api.Models;

namespace HabitTracker.Api.Validators;

public class ListDTOValidator : AbstractValidator<ListDTO>
{
    public ListDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("List name is required.")
            .MaximumLength(100).WithMessage("List name must be 100 characters or fewer.");

        RuleFor(x => x.CreatedDate)
            .NotEmpty().WithMessage("CreatedDate is required.");

        RuleForEach(x => x.ListItems)
            .SetValidator(new TaskDTOValidator());
    }
}

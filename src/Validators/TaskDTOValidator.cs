using FluentValidation;
using HabitTracker.Api.Models;

namespace HabitTracker.Api.Validators;

public class TaskDTOValidator : AbstractValidator<TaskDTO>
{
    public TaskDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Task name is required.")
            .MaximumLength(100).WithMessage("Task name must be 100 characters or fewer.");
        RuleFor(x => x.CreatedDate)
            .NotEmpty().WithMessage("CreatedDate is required.");
    }
}

using FluentValidation;
using HabitTracker.Api.Infrastructure;
using HabitTracker.Api.Requests;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Api.Validators;

public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
{
    private readonly HabitTrackerDbContext _db;

    public CreateTaskRequestValidator(HabitTrackerDbContext db)
    {
        _db = db;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Task name is required.")
            .MaximumLength(100).WithMessage("Task name must be 100 characters or fewer.")
            .MustAsync(BeUniqueName).WithMessage("Task name must be unique in the list.");

        RuleFor(x => x.ParentListId)
            .NotEmpty().WithMessage("ParentListId is required.");
    }

    private async Task<bool> BeUniqueName(CreateTaskRequest request, string name, CancellationToken cancellationToken)
    {
        return !await _db.Tasks.AnyAsync(t => t.Name == name && t.ParentListId == request.ParentListId, cancellationToken);
    }
}

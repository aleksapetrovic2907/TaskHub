using FluentValidation;
using TaskHub.Api.Models;

namespace TaskHub.Api.Validators
{
    public class UpdateTaskDtoValidator : AbstractValidator<CreateTaskDto>
    {
        public UpdateTaskDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required");

            RuleFor(task => task.DueAt)
                .GreaterThan(DateTime.UtcNow).WithMessage("Due date must be in the future")
                .When(task => task.DueAt.HasValue);
        }
    }
}

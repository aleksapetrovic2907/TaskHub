using FluentValidation;
using TaskHub.Api.Models;

namespace TaskHub.Api.Validators
{
    public class CreateTaskDtoValidator : AbstractValidator<CreateTaskDto>
    {
        public CreateTaskDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required");

            RuleFor(task => task.DueAt)
                .GreaterThan(DateTime.UtcNow).WithMessage("Due date must be in the future");
        }
    }
}

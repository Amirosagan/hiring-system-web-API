using FluentValidation;

namespace HiringSystem.Application.Jobs.Commands.AddJob;

public class AddJobCommandValidator : AbstractValidator<AddJobCommand>
{
    public AddJobCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.Description)
            .NotEmpty();

        RuleFor(v => v.TalentId)
            .NotEmpty();
    }
}
using FluentValidation;

namespace HiringSystem.Application.Authentication.Commands.JobSeekerRegister;

public class JobSeekerRegisterCommandValidator : AbstractValidator<JobSeekerRegisterCommand>
{
    public JobSeekerRegisterCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .MaximumLength(50).WithMessage("Email must not exceed 50 characters")
            .EmailAddress().WithMessage("Email is not valid");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
    }
}
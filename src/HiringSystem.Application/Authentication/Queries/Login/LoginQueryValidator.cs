using FluentValidation;
using HiringSystem.Application.Authentication.Common;

namespace HiringSystem.Application.Authentication.Queries.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .MaximumLength(50).WithMessage("Email must not exceed 50 characters")
            .EmailAddress().WithMessage("Email is not valid");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
    }
    
}
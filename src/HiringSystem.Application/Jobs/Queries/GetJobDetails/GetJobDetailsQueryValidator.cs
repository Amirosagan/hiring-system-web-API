using FluentValidation;

namespace HiringSystem.Application.Jobs.Queries.GetJobDetails;

public class GetJobDetailsQueryValidator : AbstractValidator<GetJobDetailsQuery>
{
    public GetJobDetailsQueryValidator()
    {
        RuleFor(v => v.JobId)
            .NotEmpty();
    }
}
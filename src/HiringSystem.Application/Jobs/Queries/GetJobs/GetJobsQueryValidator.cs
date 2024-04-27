using FluentValidation;

namespace HiringSystem.Application.Jobs.Queries.GetJobs;

public class GetJobsQueryValidator : AbstractValidator<GetJobsQuery>
{
    public GetJobsQueryValidator()
    {
        RuleFor(v => v.SearchTerm)
            .MaximumLength(200);

        RuleFor(v => v.SortWith)
            .MaximumLength(200);

        RuleFor(v => v.PageSize)
            .GreaterThan(0);
    }
}
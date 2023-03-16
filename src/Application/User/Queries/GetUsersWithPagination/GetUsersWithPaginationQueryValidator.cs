using FluentValidation;

namespace CleanArchitecture.Application.User.Queries.GetUsersWithPagination;

public class GetUsersWithPaginationQueryValidator : AbstractValidator<GetUsersWithPaginationQueryRequest>
{
    public GetUsersWithPaginationQueryValidator()
    {
        RuleFor(v => v.PageNumber)
            .GreaterThanOrEqualTo(1)
            .NotEmpty().NotNull();
        RuleFor(v => v.PageSize)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(50)
            .NotEmpty().NotNull();
    }
}
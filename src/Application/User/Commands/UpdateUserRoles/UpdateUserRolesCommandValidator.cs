using CleanArchitecture.Domain.Enums;
using FluentValidation;

namespace CleanArchitecture.Application.User.Commands.UpdateUserRoles;

public class UpdateUserRolesCommandValidator : AbstractValidator<UpdateUserRolesCommandRequest>
{
    public UpdateUserRolesCommandValidator()
    {
        RuleFor(v => v.Id).NotEmpty().NotNull();
        RuleFor(v => v.Roles).Must(roles =>
        {
            return roles.All(role => Enum.IsDefined(typeof(Roles), role));
        }).WithMessage("invalid role").NotEmpty().NotNull();
    }
}
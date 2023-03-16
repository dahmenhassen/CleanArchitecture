using FluentValidation;

namespace CleanArchitecture.Application.User.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommandRequest>
{
    public UpdateUserCommandValidator()
    {

        RuleFor(v => v.FirstName).NotEmpty().NotNull();
        RuleFor(v => v.LastName).NotEmpty().NotNull();
        RuleFor(v => v.ConfirmPassword)
            .Equal(u => u.Password)
            .WithMessage("'Password' and 'Confirm password' are not the same");
    }
}
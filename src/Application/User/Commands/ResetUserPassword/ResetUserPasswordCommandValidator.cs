using CleanArchitecture.Domain.Enums;
using FluentValidation;

namespace CleanArchitecture.Application.User.Commands.ResetUserPassword;

public class ResetUserPasswordCommandValidator : AbstractValidator<ResetUserPasswordCommandRequest>
{
    public ResetUserPasswordCommandValidator()
    {
        RuleFor(v => v.UserName).EmailAddress().NotEmpty().NotNull();
        RuleFor(v => v.Token).NotEmpty().NotNull();
        RuleFor(v => v.NewPassword).NotEmpty().NotNull();
        RuleFor(v => v.ConfirmPassword)
            .Equal(u => u.NewPassword)
            .WithMessage("'Password' and 'Confirm password' are not the same")
            .NotEmpty().NotNull();
    }
}
using CleanArchitecture.Domain.Enums;
using FluentValidation;

namespace CleanArchitecture.Application.User.Commands.UpdateUserPassword;

public class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommandRequest>
{
    public UpdateUserPasswordCommandValidator()
    {
        RuleFor(v => v.CurrentPassword).NotEmpty().NotNull();
        RuleFor(v => v.NewPassword).NotEmpty().NotNull();
        RuleFor(v => v.ConfirmNewPassword)
            .Equal(u => u.NewPassword)
            .WithMessage("'Password' and 'Confirm password' are not the same")
            .NotEmpty().NotNull();
        
    }
}
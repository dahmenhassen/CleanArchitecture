using CleanArchitecture.Domain.Enums;
using FluentValidation;

namespace CleanArchitecture.Application.User.Commands.ForgotUserPassword;

public class ForgotUserPasswordCommandValidator : AbstractValidator<ForgotUserPasswordCommandRequest>
{
    public ForgotUserPasswordCommandValidator()
    {
        RuleFor(v => v.UserName).EmailAddress().NotEmpty().NotNull();
        RuleFor(v => v.UrlCallback).NotEmpty().NotNull();
    }
}
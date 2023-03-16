using FluentValidation;

namespace CleanArchitecture.Application.User.Commands.DeleteUser;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommandRequest>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(v => v.Id).NotEmpty().NotNull();
    }
}
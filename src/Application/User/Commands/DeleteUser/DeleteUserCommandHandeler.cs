using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using MediatR;

namespace CleanArchitecture.Application.User.Commands.DeleteUser;

public class
    DeleteUserCommandHandler : IRequestHandler<DeleteUserCommandRequest, ServiceResult<DeleteUserCommandResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;
    private readonly ITokenService _tokenService;

    public DeleteUserCommandHandler(IApplicationDbContext context, ITokenService tokenService,
        IIdentityService identityService, ICurrentUserService currentUserService)
    {
        _context = context;
        _tokenService = tokenService;
        _identityService = identityService;
        _currentUserService = currentUserService;
    }

    public async Task<ServiceResult<DeleteUserCommandResponse>> Handle(DeleteUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        Result deleteResult = await _identityService.DeleteUserAsync(request.Id);

        if (!deleteResult.Succeeded)
        {
            throw new ValidationException(deleteResult.Errors);
        }

        return ServiceResult.Success(new DeleteUserCommandResponse { Id = request.Id });
    }
}
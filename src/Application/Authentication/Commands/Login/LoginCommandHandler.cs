using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Authentication.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, ServiceResult<LoginCommandResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(IApplicationDbContext context, ITokenService tokenService,
        IIdentityService identityService)
    {
        _context = context;
        _tokenService = tokenService;
        _identityService = identityService;
    }

    public async Task<ServiceResult<LoginCommandResponse>> Handle(LoginCommandRequest request,
        CancellationToken cancellationToken)
    {
        var userId = await _identityService.GetUserIdAsync(request.UserName);
        
        if (userId is null)
        {
            return ServiceResult.Failed<LoginCommandResponse>(ServiceError.UserNotFound);
        }

        if (!await _identityService.CheckPasswordAsync(userId, request.Password))
        {
            return ServiceResult.Failed<LoginCommandResponse>(ServiceError.WrongUserNameOrPassword);
        }

        string token = _tokenService.CreateJwtSecurityToken(userId) ?? string.Empty;

        return ServiceResult.Success(new LoginCommandResponse { Token = token });
    }
}
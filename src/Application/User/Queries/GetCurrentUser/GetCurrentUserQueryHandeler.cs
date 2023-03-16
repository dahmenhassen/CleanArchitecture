using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.User.Queries.GetCurrentUser;

[Authorize(Roles = "Admin,User")]
public class
    GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQueryRequest, ServiceResult<GetCurrentUserQueryResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public GetCurrentUserQueryHandler(IApplicationDbContext context, ITokenService tokenService,
        IIdentityService identityService, ICurrentUserService currentUserService, IMapper mapper)
    {
        _context = context;
        _tokenService = tokenService;
        _identityService = identityService;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<ServiceResult<GetCurrentUserQueryResponse>> Handle(GetCurrentUserQueryRequest request,
        CancellationToken cancellationToken)
    {
        string? currentUserId = _currentUserService.UserId;

        if (string.IsNullOrEmpty(currentUserId))
        {
            throw new UnauthorizedAccessException();
        }

        IList<string> roles = await _identityService.GetUserRoleAsync(currentUserId);
        UserInfo? userInfo = _context.UserInfos.FirstOrDefault(u => u.Id == currentUserId);

        GetCurrentUserQueryResponse? result = _mapper.Map<GetCurrentUserQueryResponse>(userInfo);
        result.Roles = roles;

        return ServiceResult.Success(result);
    }
}
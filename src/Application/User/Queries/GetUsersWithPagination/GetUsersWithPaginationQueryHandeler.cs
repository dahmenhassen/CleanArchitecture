using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Tools;
using MediatR;

namespace CleanArchitecture.Application.User.Queries.GetUsersWithPagination;

public class GetUsersWithPaginationQueryHandler : IRequestHandler<GetUsersWithPaginationQueryRequest,
    ServiceResult<PaginatedList<GetUsersWithPaginationQueryResponse>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUsersWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<PaginatedList<GetUsersWithPaginationQueryResponse>>> Handle(
        GetUsersWithPaginationQueryRequest request, CancellationToken cancellationToken)
    {
        var paginatedList = await _context.UserInfos
            .WhereIf(!string.IsNullOrEmpty(request.UserNameLike),
                u => u.UserName.Contains(request.UserNameLike!)
            )
            .OrderByIf(!string.IsNullOrEmpty(request.Sort),
                request.Sort.Trim()
            )
            .ProjectTo<GetUsersWithPaginationQueryResponse>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return ServiceResult.Success(paginatedList);
    }
}
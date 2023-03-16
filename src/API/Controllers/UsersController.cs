using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.User.Commands.CreateUser;
using CleanArchitecture.Application.User.Commands.DeleteUser;
using CleanArchitecture.Application.User.Queries.GetCurrentUser;
using CleanArchitecture.Application.User.Queries.GetUsersWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ServiceResult<CreateUserCommandResponse>>> CreateUser(
        CreateUserCommandRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet("CurrentUser")]
    public async Task<ActionResult<ServiceResult<GetCurrentUserQueryResponse>>> GetCurrentUser()
    {
        return await Mediator.Send(new GetCurrentUserQueryRequest());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResult<DeleteUserCommandResponse>>> DeleteUser(string id)
    {
        return await Mediator.Send(new DeleteUserCommandRequest(id));
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResult<PaginatedList<GetUsersWithPaginationQueryResponse>>>>
        GetUsersWithPagination([FromQuery] GetUsersWithPaginationQueryRequest request)
    {
        return await Mediator.Send(request);
    }
}
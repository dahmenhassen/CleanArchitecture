using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Tools;
using CleanArchitecture.Application.User.Commands.CreateUser;
using CleanArchitecture.Application.User.Commands.DeleteUser;
using CleanArchitecture.Application.User.Commands.ForgotUserPassword;
using CleanArchitecture.Application.User.Commands.ResetUserPassword;
using CleanArchitecture.Application.User.Commands.UpdateUser;
using CleanArchitecture.Application.User.Commands.UpdateUserPassword;
using CleanArchitecture.Application.User.Commands.UpdateUserRoles;
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

    [HttpPut("{id}")]
    public async Task<ActionResult<ServiceResult<UpdateUserCommandResponse>>> UpdateUser(string id,
        UpdateUserCommandRequest request)
    {
        Utils.CheckIsIdsAreSame(id, request.Id);
        return await Mediator.Send(request);
    }

    [HttpPut("{id}/Roles")]
    public async Task<ActionResult<ServiceResult<UpdateUserRolesCommandResponse>>> UpdateUserRoles(string id,
        UpdateUserRolesCommandRequest request)
    {
        Utils.CheckIsIdsAreSame(id, request.Id);
        return await Mediator.Send(request);
    }

    [HttpGet("CurrentUser")]
    public async Task<ActionResult<ServiceResult<GetCurrentUserQueryResponse>>> GetCurrentUser()
    {
        return await Mediator.Send(new GetCurrentUserQueryRequest());
    }

    [HttpPut("CurrentUser/UpdatePassword")]
    public async Task<ActionResult<ServiceResult<UpdateUserPasswordCommandResponse>>> UpdateUserPassword(
        UpdateUserPasswordCommandRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpPost("ForgotPassword")]
    public async Task<ActionResult<ServiceResult<ForgotUserPasswordCommandResponse>>> ForgotUserPassword(
        ForgotUserPasswordCommandRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpPost("ResetPassword")]
    public async Task<ActionResult<ServiceResult<ResetUserPasswordCommandResponse>>> ResetUserPassword(
        ResetUserPasswordCommandRequest request)
    {
        return await Mediator.Send(request);
    }
}
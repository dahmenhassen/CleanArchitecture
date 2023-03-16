using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.User.Commands.CreateUser;
using CleanArchitecture.Application.User.Queries.GetCurrentUser;
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
}
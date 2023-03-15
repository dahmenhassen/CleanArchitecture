using CleanArchitecture.Application.Authentication.Commands.Login;
using CleanArchitecture.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AuthenticationController : ApiControllerBase
{
    
    [HttpPost]
    public async Task<ActionResult<ServiceResult<LoginCommandResponse>>> Login(LoginCommandRequest request)
    {
        return await Mediator.Send(request);
    }
}
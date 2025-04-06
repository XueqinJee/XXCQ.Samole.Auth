using AuthWebServer.Config;
using AuthWebServer.ViewModel;
using ClassLibrary.Dto;
using ClassLibrary.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthWebServer.Controllers;

[ApiController]
[Route("[Controller]")]
public class AccountController : ControllerBase
{
    private readonly IAuthTokenService  _authTokenService;

    public AccountController(IAuthTokenService authTokenService)
    {
        _authTokenService = authTokenService;
    }
    
    [HttpPost("Login")]
    public async Task<Result> Login([FromBody]AccountViewModel model)
    {
        var result = await _authTokenService.CreateTokenAsync(model.UserName, model.Password);
        return Result.Success(result);
    }


    [HttpPost("refreshToken")]
    public async Task<Result> refreshToken([FromBody] TokenDto tokenDto) {
        var result = await _authTokenService.RefreshTokenAsync(tokenDto);
        return Result.Success(result);
    }
}
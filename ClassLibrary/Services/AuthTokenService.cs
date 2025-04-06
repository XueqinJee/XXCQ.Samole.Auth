using System.Buffers.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthWebServer.Config.Options;
using ClassLibrary.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model;
using Model.Entity;

namespace ClassLibrary.Services;

public class AuthTokenService(
    ILogger<AuthTokenService> _logger,
    IOptionsSnapshot<JwtBearerOptions> _jwtBearerOption,
    IOptionsSnapshot<JwtConfigOption> _jwtConfigOption,
    IMemoryCache _memoryCache,
    MyDbContext _myDbContext) : IAuthTokenService
{
    private static string RefreshTokenCacheKey => "RefreshTokenCacheKey";
    
    public async Task<TokenDto> CreateTokenAsync(string userName, string password)
    {
        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
        {
            throw new ArgumentNullException($"{nameof(userName)} || {nameof(password)} 不可为空！");
        }
        
        var user = await _myDbContext.User.Where(x => x.LoginName == userName).FirstOrDefaultAsync();
        if (user == null || user.Password != password)
        {
            throw new Exception("账号不存在或密码错误！");
        }
        
        var (refreshTokenId, refreshToken) = await CreateRefreshToken(user.Id.ToString());
        var token = await CreateJwtToken(user, refreshToken);
        var tokenDto = new TokenDto();
        tokenDto.RefreshToken = refreshToken;
        tokenDto.Token = token;
        tokenDto.UserName = user.Name;
        tokenDto.LoginTime = DateTime.UtcNow;
        return tokenDto;
    }

    public async Task<TokenDto> RefreshTokenAsync(TokenDto tokenDto)
    {
        var param = _jwtBearerOption.Get(JwtBearerDefaults.AuthenticationScheme).TokenValidationParameters.Clone();
        param.ValidateLifetime = false;

        ClaimsPrincipal? principal = null;
        // 验证Token
        var handler = new JwtSecurityTokenHandler();
        try {
            principal = handler.ValidateToken(tokenDto.Token, param, out _);
        } catch (Exception ex) {
            _logger.LogWarning(ex.Message);
            throw new Exception($"Invalid access token");
        }

        // 获取RefreshToken
        var userId = principal.Claims.Where(x => x.Type == "UserId").FirstOrDefault()?.Value;
        if (string.IsNullOrEmpty(userId)) {
            throw new Exception("未获取到用户标识");
        }
        var user = _myDbContext.User.FirstOrDefault(x => x.Id == int.Parse(userId!));
        if(user == null) {
            throw new Exception("非法参数，非获取到关联用户！");
        }

        string key = CreateRefreshTokenKey(user.Id.ToString());
        var refreshToken = principal.Claims.Where(x => x.Type == RefreshTokenCacheKey).FirstOrDefault()?.Value;
        if (string.IsNullOrEmpty(key)) {
            throw new Exception($"Invalid access token");
        }

        var authRefreshToken = _memoryCache.Get<string>(key);
        if(refreshToken == null || authRefreshToken != refreshToken) {
            throw new Exception("Invalid refresh token");
        }
        _memoryCache.Remove(key);
        return await CreateTokenAsync(user.LoginName!, user.Password!);
    }

    /// <summary>
    /// 创建Token
    /// </summary>
    /// <param name="user">用户User</param>
    /// <param name="refreshTokenId">刷新Token的Key</param>
    /// <returns>Token</returns>
    private Task<string> CreateJwtToken(User user, string refreshToken)
    {
        
        var decriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Name!),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim("UserId", user.Id.ToString()),
                new Claim(RefreshTokenCacheKey, refreshToken),
            }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            Issuer = _jwtBearerOption.Get(JwtBearerDefaults.AuthenticationScheme).TokenValidationParameters.ValidIssuer,
            Audience = _jwtBearerOption.Get(JwtBearerDefaults.AuthenticationScheme).TokenValidationParameters.ValidAudience,
            
            SigningCredentials = new SigningCredentials(_jwtConfigOption.Value.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256)
        };
        
        var handler = _jwtBearerOption.Value.TokenHandlers.OfType<JwtSecurityTokenHandler>().FirstOrDefault()
            ?? new JwtSecurityTokenHandler();
        var securityToken = handler.CreateJwtSecurityToken(decriptor);
        var token = handler.WriteToken(securityToken);
        return Task.FromResult(token);
    }
    
    /// <summary>
    /// 创建刷新Token
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    private Task<(string refreshTokenId, string refreshToken)> CreateRefreshToken(string userId)
    {
        var refreshTokenId = CreateRefreshTokenKey(userId);
        
        var token = Guid.NewGuid().ToString("N");
        var refreshToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
        _memoryCache.Set(refreshTokenId, refreshToken, TimeSpan.FromDays(1));   
        return Task.FromResult((refreshTokenId, refreshToken));
    }

    private string CreateRefreshTokenKey(string userId)
    {
        string key = $"{RefreshTokenCacheKey}_{userId}";
        return key;
    }
}
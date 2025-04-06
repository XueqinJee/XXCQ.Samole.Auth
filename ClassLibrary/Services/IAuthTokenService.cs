using ClassLibrary.Dto;

namespace ClassLibrary.Services;

public interface IAuthTokenService
{
    Task<TokenDto> CreateTokenAsync(string userName, string password);
    Task<TokenDto> RefreshTokenAsync(TokenDto tokenDto);
}
namespace ClassLibrary.Dto;

public class TokenDto
{
    public string? UserName { get; set; }
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime LoginTime { get; set; }
}
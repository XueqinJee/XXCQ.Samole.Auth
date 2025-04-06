using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AuthWebServer.Config.Options;

public class JwtConfigOption
{
    public static readonly string SectionName = "JwtConfig";
    private static Encoding _defaultEncoding = Encoding.UTF8;
    
    [Required(ErrorMessage = $"{nameof(Issuer)} 不可为空")]
    public string? Issuer { get; set; }
    
    [Required(ErrorMessage = $"{nameof(Audience)} 不可为空")]
    public string? Audience { get; set; }
    
    [Required(ErrorMessage = $"{nameof(SecurityKey)} 不可为空")]
    public string? SecurityKey { get; set; }
    
    public Encoding Encoding { get; set; } = _defaultEncoding;
    public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Encoding.GetBytes(SecurityKey));
}
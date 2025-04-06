using System.Security.Claims;
using AuthWebServer.Config.Options;
using ClassLibrary.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace AuthWebServer.Config.Extensions;

public static class JwtServiceExtension
{

    public static void AddJwtService(this IServiceCollection services, IConfiguration configuration)
    {
        
        var jwtConfig = configuration.GetSection(JwtConfigOption.SectionName)
            .Get<JwtConfigOption>();

        services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidAlgorithms = new [] {SecurityAlgorithms.HmacSha256, SecurityAlgorithms.RsaSha256 },
                ValidTypes = new []{JwtConstants.HeaderType},
                ValidIssuer = jwtConfig?.Issuer,
                ValidateIssuer = true,
                ValidAudience = jwtConfig?.Audience,
                ValidateAudience = true,
                IssuerSigningKey = jwtConfig?.SymmetricSecurityKey,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                NameClaimType = ClaimTypes.Name,
                RoleClaimType = ClaimTypes.Role,
                ClockSkew = TimeSpan.FromMinutes(1)
            };

            options.UseSecurityTokenValidators = true;
            options.SaveToken = true;
        });

        services.AddScoped<IAuthTokenService, AuthTokenService>();
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme);
    }
}
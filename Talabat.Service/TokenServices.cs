using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;

namespace Talabat.Service;

public class TokenServices : ITokenService
{
    public IConfiguration Configuration { get; }

    public TokenServices(IConfiguration configuration   )
    {
        Configuration = configuration;
    }
    public async  Task<string> CreateToken(AppUser user , UserManager<AppUser> userManager)
    {
        
        // Token ==> Claim (Private , Registered ) , Header (Algo , Typ == "JWT") , Key
        
        // Registerd Claim const for all users ==> [Put it in AppSetting] ==>[Issuer , audience , Expire , ....] 
        
        // Private Claims  

        var authClaims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.GivenName, user.DisplayName),

        };
        var userRoles = await userManager.GetRolesAsync(user);
        foreach (var role in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role , role));
        }

        // Secrete Key 
        var authkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]));


        var Token = new JwtSecurityToken(
            issuer: Configuration["JWT:ValidIssuer"],
            audience: Configuration["JWT:ValidAudience"],
            expires: DateTime.UtcNow.AddDays(Convert.ToDouble(Configuration["JWT:DurationInDays"])),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authkey, SecurityAlgorithms.HmacSha256Signature)

        );
        return new JwtSecurityTokenHandler().WriteToken(Token);

    }
}
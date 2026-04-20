using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;

namespace Talabat.Core.Services;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user , UserManager<AppUser> userManager );
}
using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Idnetity;

public class AppIdentityDbContextSeed
{
    public static async Task SeedUsersAsync(UserManager<AppUser>userManager )
    {
        if (!userManager.Users.Any())
        {
            var user = new AppUser()
            {
                DisplayName = "Esma3el Ghanem",
                Email = "ghanemesmaiel@gmail.com",
                UserName = "EsmaielGhanem",
                PhoneNumber = "01203591585"
            };
            await userManager.CreateAsync(user ,"Abo_Sa3eed123" );
        }
        
    }
}
using IdentityModel;
using IdentityService.Data;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IdentityService;

public class SeedData
{
    public static void EnsureSeedData(WebApplication app)
    {
        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();

        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var john = userMgr.FindByNameAsync("john").Result;
        if (john == null)
        {
            john = new ApplicationUser
            {
                UserName = "john",
                Email = "JohnDoe@email.com",
                EmailConfirmed = true,
            };
            var result = userMgr.CreateAsync(john, "Pass123$").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(john, [
                        new(JwtClaimTypes.Name, "John Doe"),
                new(JwtClaimTypes.GivenName, "John"),
                new(JwtClaimTypes.FamilyName, "Doe"),
                new(JwtClaimTypes.WebSite, "http://john.com"),
            ]).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            Log.Debug("jhon created");
        }
        else
        {
            Log.Debug("john already exists");
        }

        var jane = userMgr.FindByNameAsync("jane").Result;
        if (jane == null)
        {
            jane = new ApplicationUser
            {
                UserName = "jane",
                Email = "JaneDoe@email.com",
                EmailConfirmed = true
            };
            var result = userMgr.CreateAsync(jane, "Pass123$").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(jane, [
                        new(JwtClaimTypes.Name, "Jane Doe"),
                new(JwtClaimTypes.GivenName, "Jane"),
                new(JwtClaimTypes.FamilyName, "Doe"),
                new(JwtClaimTypes.WebSite, "http://jane.com"),
                new("location", "somewhere")
                    ]).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            Log.Debug("jane created");
        }
        else
        {
            Log.Debug("jane already exists");
        }
    }
}

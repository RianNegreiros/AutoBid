using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Services;

public class CustomProfileService : IProfileService
{
  private readonly UserManager<IdentityUser> _userManager;

  public CustomProfileService(UserManager<IdentityUser> userManager)
  {
    _userManager = userManager;
  }

  public async Task GetProfileDataAsync(ProfileDataRequestContext context)
  {
    var user = await _userManager.GetUserAsync(context.Subject);
    var existingClaims = await _userManager.GetClaimsAsync(user);

    var claims = new List<Claim>
    {
        new("username", user.UserName),
    };

    context.IssuedClaims.AddRange(claims);
    context.IssuedClaims.Add(existingClaims.FirstOrDefault(x => x.Type == JwtClaimTypes.Name));
  }

  public Task IsActiveAsync(IsActiveContext context)
  {
    context.IsActive = true;
    return Task.CompletedTask;
  }
}

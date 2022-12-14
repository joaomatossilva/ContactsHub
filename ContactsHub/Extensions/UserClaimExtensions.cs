namespace ContactsHub.Extensions;

using System.Security.Claims;

public static class UserClaimExtensions
{
    public static string GetUserId(this ClaimsPrincipal claimsPrincipal) => 
        claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ??
        throw new Exception("Could not find UserId Claim");
    
    public static string GetDisplayName(this ClaimsPrincipal claimsPrincipal) => 
        claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value ??
        claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ??
        throw new Exception("Could not find Name Claim");
}
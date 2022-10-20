namespace ContactsHub.Data.Model;

using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

public class User : IdentityUser
{
    [PersonalData]
    [Required]
    public string Name { get; set; }
}
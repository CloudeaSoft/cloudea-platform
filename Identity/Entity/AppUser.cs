using Microsoft.AspNetCore.Identity;

namespace Identity.Entity;

public class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

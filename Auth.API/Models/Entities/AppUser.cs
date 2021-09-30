using Microsoft.AspNetCore.Identity;

namespace Auth.API.Models.Entities
{
    public class AppUser : IdentityUser<System.Guid>
    {
    }
    public class pic : IdentityUserRole<System.Guid>
    {

    }
}

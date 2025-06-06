using Microsoft.AspNetCore.Identity;

namespace RSW.WebApp.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public Group? group { get; set; } = null;
    }

}

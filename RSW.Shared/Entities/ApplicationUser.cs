using Microsoft.AspNetCore.Identity;

namespace RSW.Shared.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public Group? group { get; set; } = null;
        public List<VolunteerAssignment> volunteerAssignments { get; set; }
    }

}

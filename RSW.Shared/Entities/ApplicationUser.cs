using Microsoft.AspNetCore.Identity;

namespace RSW.Shared.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public Unit? Unit { get; set; } = null;
        public Guid? UnitId { get; set; } = null;
        public List<VolunteerAssignment>? volunteerAssignments { get; set; }
        public string? Firstname { get; set; } = null!;
        public string? Lastname { get; set; } = null!;
    }

}

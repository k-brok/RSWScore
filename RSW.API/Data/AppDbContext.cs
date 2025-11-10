using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RSW.Shared.Entities;

namespace RSW.API.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Association> Associations { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Criteria> Criterias { get; set; } = null!;
        public DbSet<Edition> Editions { get; set; } = null!;
        public DbSet<Unit> Units { get; set; } = null!;
        public DbSet<JurySlot> JurySlots { get; set; } = null!;
        public DbSet<Patrol> Patrols { get; set; } = null!;
        public DbSet<Score> Scores { get; set; } = null!;
        public DbSet<Scout> Scouts { get; set; } = null!;
        public DbSet<SignupCode> SignupCodes { get; set; } = null!;
        public DbSet<SubCategory> SubCategories { get; set; } = null!;
        public DbSet<SubGroup> SubGroups { get; set; } = null!;
        public DbSet<WebSetting> WebSettings { get; set; } = null!;
        public DbSet<VolunteerTask> VolunteerTasks { get; set; } = null!;
        public DbSet<VolunteerAssignment> VolunteerAssignments { get; set; } = null!;
        public DbSet<PendingUnitLinkRequest> UnitLinkRequests => Set<PendingUnitLinkRequest>();
        public DbSet<EmailConfig> EmailConfigs { get; set; } = null!;
    }
}

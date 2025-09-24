namespace RSW.Shared.Entities
{
    public class VolunteerAssignment : BaseEntity
    {
        public ApplicationUser? User { get; set; }
        public string? UserId { get; set; }
        public Edition? Edition { get; set; }
        public Guid EditionId { get; set; }
        public VolunteerTask? Task { get; set; }
        public Guid TaskId { get; set; }
        public bool HasVOG { get; set; } = false;
        
    }
}

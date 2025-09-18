namespace RSW.Shared.Entities
{
    public class Group : BaseEntity
    {
        public required string Name { get; set; }
        public Guid AssociationId { get; set; }
        public Association Association { get; set; } = null!;
        public List<Patrol> Patrols { get; set; } = new List<Patrol>();
    }
}

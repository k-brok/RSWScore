namespace RSW.Shared.Entities
{
    public class Association : BaseEntity
    {
        public required string Name { get; set; }
        public string Abbreviation { get; set; } = string.Empty;
        public List<Group> Groups { get; set; } = new List<Group>();
    }
}

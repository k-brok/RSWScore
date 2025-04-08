namespace RSW.WebApp.Entities
{
    public class Association : BaseEntity
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}

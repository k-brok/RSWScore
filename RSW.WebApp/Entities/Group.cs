namespace RSW.WebApp.Entities
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        public int AssociationId { get; set; }
        public Association Association { get; set; }
        public List<Patrol> Patrols { get; set; }
    }
}

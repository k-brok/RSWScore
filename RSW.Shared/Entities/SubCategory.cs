namespace RSW.Shared.Entities
{
    public class SubCategory : BaseEntity
    {
        public required string Name { get; set; }
        public List<Criteria> criterias {  get; set; } = new List<Criteria>();
        public required Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}

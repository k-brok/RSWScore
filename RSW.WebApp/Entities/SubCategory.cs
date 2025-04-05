namespace RSW.WebApp.Entities
{
    public class SubCategory : BaseEntity
    {
        public string Name { get; set; }
        public List<Criteria> criterias {  get; set; } = new List<Criteria>();
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

namespace RSW.Shared.Entities
{
    public class SignupCode : BaseEntity
    {
        public Guid GroupId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Lock { get; set; }
    }
}

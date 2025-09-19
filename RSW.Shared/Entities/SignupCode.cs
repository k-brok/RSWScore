namespace RSW.Shared.Entities
{
    public class SignupCode : BaseEntity
    {
        public required string Code { get; set; }
        public Guid GroupId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Lock { get; set; }
    }
}

namespace RSW.WebApp.Entities
{
    public class SignupCode : BaseEntity
    {
        public string Code { get; set; }
        public int GroupId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Lock { get; set; }
    }
}

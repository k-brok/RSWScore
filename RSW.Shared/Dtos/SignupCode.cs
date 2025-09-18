namespace RSW.Shared.Dto
{
    public class SignupCodeDto : BaseEntityDto
    {
        public required string Code { get; set; }
        public Guid GroupId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Lock { get; set; }
    }
}

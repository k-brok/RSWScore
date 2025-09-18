using RSW.Shared.Entities;

namespace RSW.Shared.Dto
{
    public class SignupCodeDto : BaseEntityDto
    {
        public required string Code { get; set; }
        public Guid GroupId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Lock { get; set; }
    }
    public static class SignupCodeExtensions
    {
        public static SignupCodeDto ToDto(this SignupCode signupcode)
        {
            return new SignupCodeDto
            {
                Id = signupcode.Id,
                Code = signupcode.Code,
                GroupId = signupcode.GroupId,
                ExpiryDate = signupcode.ExpiryDate,
                Lock = signupcode.Lock
            };
        }

        public static SignupCode ToEntity(this SignupCodeDto signupcode)
        {
            return new SignupCode
            {
                Id = signupcode.Id,
                Code = signupcode.Code,
                GroupId = signupcode.GroupId,
                ExpiryDate = signupcode.ExpiryDate,
                Lock = signupcode.Lock
            };
        }
    }
}

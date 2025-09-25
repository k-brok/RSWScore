using System.Text.Json.Serialization;

namespace RSW.Shared.Entities
{
    public class SignupCode : BaseEntity
    {
        [JsonIgnore]
        public Unit? Unit { get; set; }
        public Guid UnitId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Lock { get; set; }
    }
}

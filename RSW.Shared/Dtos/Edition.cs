namespace RSW.Shared.Dto
{
    public class EditionDto : BaseEntityDto
    {
        public DateOnly RSWStartDate { get; set; }
        public DateOnly? LSWStartDate { get; set; } = null;
        public String? Theme { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
    }
}

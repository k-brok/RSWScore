namespace RSW.Shared.Dto
{
    public class SubCategoryDto : BaseEntityDto
    {
        public required string Name { get; set; }
        public required Guid CategoryId { get; set; }
    }
}

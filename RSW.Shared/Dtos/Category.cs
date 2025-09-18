namespace RSW.Shared.Dto
{
    public class CategoryDto : BaseEntityDto
    {
        public required string Name { get; set; }
        public int Weight { get; set; }
    }
}

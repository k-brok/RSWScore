using RSW.Shared.Entities;

namespace RSW.Shared.Dto
{
    public class EditionDto : BaseEntityDto
    {
        public DateOnly RSWStartDate { get; set; }
        public DateOnly? LSWStartDate { get; set; } = null;
        public String? Theme { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
    }
    public static class EditionExtensions
    {
        public static EditionDto ToDto(this Edition edition)
        {
            return new EditionDto
            {
                Id = edition.Id,
                RSWStartDate = edition.RSWStartDate,
                LSWStartDate = edition.LSWStartDate,
                Theme = edition.Theme,
                IsActive = edition.IsActive
            };
        }

        public static Edition ToEntity(this EditionDto edition)
        {
            return new Edition
            {
                Id = edition.Id,
                RSWStartDate = edition.RSWStartDate,
                LSWStartDate = edition.LSWStartDate,
                Theme = edition.Theme,
                IsActive = edition.IsActive
            };
        }
    }
}

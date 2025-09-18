using RSW.Shared.Entities;

namespace RSW.Shared.Dto
{
    public class GroupDto : BaseEntityDto
    {
        public required string Name { get; set; }
        public Guid AssociationId { get; set; }
    }
    public static class GroupExtensions
    {
        public static GroupDto ToDto(this Group group)
        {
            return new GroupDto
            {
                Id = group.Id,
                Name = group.Name,
                AssociationId = group.AssociationId
            };
        }

        public static Group ToEntity(this GroupDto group)
        {
            return new Group
            {
                Id = group.Id,
                Name = group.Name,
                AssociationId = group.AssociationId
            };
        }
    }
}

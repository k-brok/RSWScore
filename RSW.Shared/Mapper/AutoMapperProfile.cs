using AutoMapper;
using RSW.Shared.Dto;
using RSW.Shared.Entities;

namespace RSW.Shared.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Association, AssociationReadDto>();
            CreateMap<Category, CategoryReadDto>();
            CreateMap<Criteria, CriteriaReadDto>();
            CreateMap<Edition, EditionReadDto>();
            CreateMap<Group, GroupReadDto>();
            CreateMap<JurySlot, JurySlotReadDto>();
            CreateMap<Patrol, PatrolReadDto>();
            CreateMap<Score, ScoreReadDto>();
            CreateMap<Scout, ScoutReadDto>();
            CreateMap<SignupCode, SignupCodeReadDto>();
            CreateMap<SubCategory, SubCategoryReadDto>();
            CreateMap<SubGroup, SubGroupReadDto>();
            CreateMap<WebSetting, WebSettingReadDto>();
        }
    }
}

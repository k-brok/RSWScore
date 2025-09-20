using AutoMapper;
using RSW.Shared.Dto;
using RSW.Shared.Entities;

namespace RSW.Shared.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Association, AssociationDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Criteria, CriteriaDto>();
            CreateMap<Edition, EditionDto>();
            CreateMap<Group, GroupDto>();
            CreateMap<JurySlot, JurySlotDto>();
            CreateMap<Patrol, PatrolDto>();
            CreateMap<Score, ScoreDto>();
            CreateMap<Scout, ScoutDto>();
            CreateMap<SignupCode, SignupCodeDto>();
            CreateMap<SubCategory, SubCategoryDto>();
            CreateMap<SubGroup, SubGroupDto>();
            CreateMap<WebSetting, WebSettingDto>();
        }
    }
}

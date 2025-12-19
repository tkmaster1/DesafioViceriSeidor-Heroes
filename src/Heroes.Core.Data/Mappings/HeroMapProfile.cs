using AutoMapper;
using Heroes.Common.Util.DTOs;
using Heroes.Common.Util.Entities;
using Heroes.Core.Application.DTOs;
using Heroes.Core.Application.DTOs.Filters;
using Heroes.Core.Application.DTOs.Request;
using Heroes.Core.Domain.Entities;
using Heroes.Core.Domain.Filters;

namespace Heroes.Core.Data.Mappings;

public class HeroMapProfile : Profile
{
    public HeroMapProfile()
    {
        CreateHeroProfile();
    }

    private void CreateHeroProfile()
    {
        CreateMap<HeroDTO, Hero>().ReverseMap();
        CreateMap<HeroFilterDTO, HeroFilter>();
        CreateMap<HeroRequestDTO, Hero>();
        
        CreateMap<Pagination<Hero>, PaginationDTO<HeroDTO>>()
       .AfterMap((source, converted, context) =>
       {
           converted.Result = context.Mapper.Map<List<HeroDTO>>(source.Result);
       });
    }
}
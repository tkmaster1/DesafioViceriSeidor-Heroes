using AutoMapper;
using Heroes.Common.Util.DTOs;
using Heroes.Common.Util.Entities;
using Heroes.Core.Application.DTOs;
using Heroes.Core.Application.DTOs.Filters;
using Heroes.Core.Application.DTOs.Request;
using Heroes.Core.Domain.Entities;
using Heroes.Core.Domain.Filters;

namespace Heroes.Core.Data.Mappings;

public class SuperpowerMapProfile : Profile
{
    public SuperpowerMapProfile()
    {
        CreateSuperpowerProfile();
    }

    private void CreateSuperpowerProfile()
    {
        CreateMap<SuperpowerDTO, Superpower>().ReverseMap();
        CreateMap<SuperpowerFilterDTO, SuperpowerFilter>();
        CreateMap<SuperpowerRequestDTO, Superpower>();

        CreateMap<Pagination<Superpower>, PaginationDTO<SuperpowerDTO>>()
       .AfterMap((source, converted, context) =>
       {
           converted.Result = context.Mapper.Map<List<SuperpowerDTO>>(source.Result);
       });
    }
}
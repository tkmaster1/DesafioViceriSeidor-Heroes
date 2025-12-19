using AutoMapper;
using Heroes.Common.Util.DTOs;
using Heroes.Common.Util.Messages;
using Heroes.Common.Util.Response;
using Heroes.Core.Application.DTOs;
using Heroes.Core.Application.DTOs.Filters;
using Heroes.Core.Application.DTOs.Request;
using Heroes.Core.Application.Facades.Interfaces;
using Heroes.Core.Domain.Entities;
using Heroes.Core.Domain.Filters;
using Heroes.Core.Domain.Interfaces.Services;

namespace Heroes.Core.Application.Facades;

public class HeroFacade : IHeroFacade
{
    #region Properties

    private readonly IMapper _mapper;
    private readonly IHeroAppService _heroApp;

    #endregion

    #region Constructor

    public HeroFacade(IMapper mapper,
                      IHeroAppService heroApp)
    {
        _mapper = mapper;
        _heroApp = heroApp;
    }

    #endregion

    #region Methods

    public async Task<PaginationDTO<HeroDTO>> ListByFiltersAsync(HeroFilterDTO filterDto)
    {
        var result = await _heroApp.ListByFiltersAsync(_mapper.Map<HeroFilter>(filterDto));

        var resultDto = _mapper.Map<PaginationDTO<HeroDTO>>(result);

        return resultDto;
    }

    public async Task<HeroDTO> GetByCodeAsync(int code)
    {
        var heroDomain = await _heroApp.GetByCodeAsync(code);

        return _mapper.Map<HeroDTO>(heroDomain);
    }

    public async Task<int> CreateHeroAsync(HeroRequestDTO requestDTO)
    {
        var heroDomain = _mapper.Map<Hero>(requestDTO);

        return await _heroApp.AddHeroAsync(heroDomain);
    }

    public async Task<bool> UpdateHeroAsync(HeroRequestDTO requestDTO)
    {
        var heroDomain = _mapper.Map<Hero>(requestDTO);

        return await _heroApp.EditHeroAsync(heroDomain);
    }

    public async Task<ResponseMessage> RemoveHeroAsync(int code)
    {
        var result = await _heroApp.DeleteHeroAsync(code);

        return new ResponseMessage
        {
            Message = result ? ValidationMessages.MSG_SUCCESSFUL("Exclusão") : ValidationMessages.MSG_FAILED("Exclusão")
        };
    }

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion
}
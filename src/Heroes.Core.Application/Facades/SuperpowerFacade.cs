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

public class SuperpowerFacade : ISuperpowerFacade
{
    #region Properties

    private readonly IMapper _mapper;
    private readonly ISuperpowerAppService _superpowerApp;

    #endregion

    #region Constructor

    public SuperpowerFacade(IMapper mapper,
                      ISuperpowerAppService superpowerApp)
    {
        _mapper = mapper;
        _superpowerApp = superpowerApp;
    }

    #endregion

    #region Methods

    public async Task<PaginationDTO<SuperpowerDTO>> ListByFiltersAsync(SuperpowerFilterDTO filterDto)
    {
        var result = await _superpowerApp.ListByFiltersAsync(_mapper.Map<SuperpowerFilter>(filterDto));

        var resultDto = _mapper.Map<PaginationDTO<SuperpowerDTO>>(result);

        return resultDto;
    }

    public async Task<SuperpowerDTO> GetByCodeAsync(int code)
    {
        var superpowerDomain = await _superpowerApp.GetByCodeAsync(code);

        return _mapper.Map<SuperpowerDTO>(superpowerDomain);
    }

    public async Task<int> CreateSuperpowerAsync(SuperpowerRequestDTO requestDTO)
    {
        var superpowerDomain = _mapper.Map<Superpower>(requestDTO);

        return await _superpowerApp.AddSuperpowerAsync(superpowerDomain);
    }

    public async Task<bool> UpdateSuperpowerAsync(SuperpowerRequestDTO requestDTO)
    {
        var superpowerDomain = _mapper.Map<Superpower>(requestDTO);

        return await _superpowerApp.EditSuperpowerAsync(superpowerDomain);
    }

    public async Task<ResponseMessage> RemoveSuperpowerAsync(int code)
    {
        var result = await _superpowerApp.DeleteSuperpowerAsync(code);

        return new ResponseMessage
        {
            Message = result ? ValidationMessages.MSG_SUCCESSFUL("Exclusão") : ValidationMessages.MSG_FAILED("Exclusão")
        };
    }

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion
}
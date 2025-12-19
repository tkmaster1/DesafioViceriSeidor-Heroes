using Heroes.Common.Util.DTOs;
using Heroes.Common.Util.Response;
using Heroes.Common.Util.Services;
using Heroes.Core.Application.DTOs;
using Heroes.Core.Application.DTOs.Filters;
using Heroes.Core.Application.DTOs.Request;
using Heroes.Core.Application.Facades.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Heroes.Core.WebApi.Controllers;

[Route("api/[controller]")]
public class SuperpowerController : MainAPIController
{
    #region Properties

    private readonly ISuperpowerFacade _superpowerFacade;

    #endregion

    #region Constructor

    public SuperpowerController(ISuperpowerFacade superpowerFacade,
        INotificationHandler<Notification> notificador) : base(notificador)
    {
        _superpowerFacade = superpowerFacade;
    }

    #endregion

    #region Methods Public

    [HttpPost("getSuperpowers")]
    [Consumes("application/Json")]
    [Produces("application/Json")]
    [ProducesResponseType(typeof(ResponseSuccess<PaginationDTO<SuperpowerDTO>>), 200)]
    [ProducesResponseType(typeof(ResponseFailure), 400)]
    [ProducesResponseType(typeof(ResponseFailure), 403)]
    [ProducesResponseType(typeof(ResponseFailure), 409)]
    [ProducesResponseType(typeof(ResponseFailure), 500)]
    [ProducesResponseType(typeof(ResponseFailure), 502)]
    public async Task<IActionResult> GetSuperpowers([FromBody] SuperpowerFilterDTO filterDTO)
        => CustomResponse(await _superpowerFacade.ListByFiltersAsync(filterDTO));

    [HttpGet("getSuperpowerByCode/{code}")]
    [Consumes("application/Json")]
    [Produces("application/Json")]
    [ProducesResponseType(typeof(ResponseSuccess<SuperpowerDTO>), 200)]
    [ProducesResponseType(typeof(ResponseFailure), 400)]
    [ProducesResponseType(typeof(ResponseFailure), 403)]
    [ProducesResponseType(typeof(ResponseFailure), 409)]
    [ProducesResponseType(typeof(ResponseFailure), 500)]
    [ProducesResponseType(typeof(ResponseFailure), 502)]
    public async Task<IActionResult> GetSuperpowerByCode(int code)
        => CustomResponse(await _superpowerFacade.GetByCodeAsync(code));

    [HttpPost("saveNewSuperpower")]
    [Consumes("application/Json")]
    [Produces("application/Json")]
    [ProducesResponseType(typeof(ResponseBaseEntity), 200)]
    [ProducesResponseType(typeof(ResponseFailure), 400)]
    [ProducesResponseType(typeof(ResponseFailure), 403)]
    [ProducesResponseType(typeof(ResponseFailure), 409)]
    [ProducesResponseType(typeof(ResponseFailure), 500)]
    [ProducesResponseType(typeof(ResponseFailure), 502)]
    public async Task<IActionResult> SaveNewSuperpower([FromBody] SuperpowerRequestDTO requestDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await _superpowerFacade.CreateSuperpowerAsync(requestDTO);

        return CustomResponse(id);
    }

    [HttpPut("editSuperpower")]
    [Consumes("application/Json")]
    [Produces("application/Json")]
    [ProducesResponseType(typeof(ResponseBaseEntity), 200)]
    [ProducesResponseType(typeof(ResponseFailure), 400)]
    [ProducesResponseType(typeof(ResponseFailure), 403)]
    [ProducesResponseType(typeof(ResponseFailure), 409)]
    [ProducesResponseType(typeof(ResponseFailure), 500)]
    [ProducesResponseType(typeof(ResponseFailure), 502)]
    public async Task<IActionResult> ChangeHero([FromBody] SuperpowerRequestDTO requestDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return CustomResponse(await _superpowerFacade.UpdateSuperpowerAsync(requestDTO));
    }

    [HttpDelete("deleteSuperpower/{code}")]
    [Consumes("application/Json")]
    [Produces("application/Json")]
    [ProducesResponseType(typeof(ResponseBaseEntity), 200)]
    [ProducesResponseType(typeof(ResponseFailure), 400)]
    [ProducesResponseType(typeof(ResponseFailure), 403)]
    [ProducesResponseType(typeof(ResponseFailure), 409)]
    [ProducesResponseType(typeof(ResponseFailure), 500)]
    [ProducesResponseType(typeof(ResponseFailure), 502)]
    public async Task<IActionResult> DeleteSuperpower(int code)
       => CustomResponse(await _superpowerFacade.RemoveSuperpowerAsync(code));

    #endregion
}
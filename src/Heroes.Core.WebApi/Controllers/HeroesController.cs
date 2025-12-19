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
public class HeroesController : MainAPIController
{
    #region Properties

    private readonly IHeroFacade _heroFacade;

    #endregion

    #region Constructor

    public HeroesController(IHeroFacade heroFacade,
        INotificationHandler<Notification> notificador) : base(notificador)
    {
        _heroFacade = heroFacade;
    }

    #endregion

    #region Methods Public

    [HttpPost("getHeroes")]
    [Consumes("application/Json")]
    [Produces("application/Json")]
    [ProducesResponseType(typeof(ResponseSuccess<PaginationDTO<HeroDTO>>), 200)]
    [ProducesResponseType(typeof(ResponseFailure), 400)]
    [ProducesResponseType(typeof(ResponseFailure), 403)]
    [ProducesResponseType(typeof(ResponseFailure), 409)]
    [ProducesResponseType(typeof(ResponseFailure), 500)]
    [ProducesResponseType(typeof(ResponseFailure), 502)]
    public async Task<IActionResult> GetHeroes([FromBody] HeroFilterDTO filterDTO)
        => CustomResponse(await _heroFacade.ListByFiltersAsync(filterDTO));

    [HttpGet("getHeroByCode/{code}")]
    [Consumes("application/Json")]
    [Produces("application/Json")]
    [ProducesResponseType(typeof(ResponseSuccess<HeroDTO>), 200)]
    [ProducesResponseType(typeof(ResponseFailure), 400)]
    [ProducesResponseType(typeof(ResponseFailure), 403)]
    [ProducesResponseType(typeof(ResponseFailure), 409)]
    [ProducesResponseType(typeof(ResponseFailure), 500)]
    [ProducesResponseType(typeof(ResponseFailure), 502)]
    public async Task<IActionResult> GetHeroByCode(int code)
        => CustomResponse(await _heroFacade.GetByCodeAsync(code));

    [HttpPost("saveNewHero")]
    [Consumes("application/Json")]
    [Produces("application/Json")]
    [ProducesResponseType(typeof(ResponseBaseEntity), 200)]
    [ProducesResponseType(typeof(ResponseFailure), 400)]
    [ProducesResponseType(typeof(ResponseFailure), 403)]
    [ProducesResponseType(typeof(ResponseFailure), 409)]
    [ProducesResponseType(typeof(ResponseFailure), 500)]
    [ProducesResponseType(typeof(ResponseFailure), 502)]
    public async Task<IActionResult> SaveNewHero([FromBody] HeroRequestDTO requestDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await _heroFacade.CreateHeroAsync(requestDTO);

        return CustomResponse(id);
    }

    [HttpPut("editHero")]
    [Consumes("application/Json")]
    [Produces("application/Json")]
    [ProducesResponseType(typeof(ResponseBaseEntity), 200)]
    [ProducesResponseType(typeof(ResponseFailure), 400)]
    [ProducesResponseType(typeof(ResponseFailure), 403)]
    [ProducesResponseType(typeof(ResponseFailure), 409)]
    [ProducesResponseType(typeof(ResponseFailure), 500)]
    [ProducesResponseType(typeof(ResponseFailure), 502)]
    public async Task<IActionResult> ChangeHero([FromBody] HeroRequestDTO requestDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return CustomResponse(await _heroFacade.UpdateHeroAsync(requestDTO));
    }

    [HttpDelete("deleteHero/{code}")]
    [Consumes("application/Json")]
    [Produces("application/Json")]
    [ProducesResponseType(typeof(ResponseBaseEntity), 200)]
    [ProducesResponseType(typeof(ResponseFailure), 400)]
    [ProducesResponseType(typeof(ResponseFailure), 403)]
    [ProducesResponseType(typeof(ResponseFailure), 409)]
    [ProducesResponseType(typeof(ResponseFailure), 500)]
    [ProducesResponseType(typeof(ResponseFailure), 502)]
    public async Task<IActionResult> DeleteHero(int code)
       => CustomResponse(await _heroFacade.RemoveHeroAsync(code));

    #endregion
}
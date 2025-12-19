using Heroes.Common.Util.Services;
using Heroes.Core.Application.DTOs.Filters;
using Heroes.Core.Application.Facades.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Heroes.Web.Vue.UI.Controllers;

[Route("[controller]/[Action]")]
public class SuperheroesController : MainController
{   
    #region Properties

    private readonly IApiFacade _apiFacade;

    #endregion

    #region Constructor

    public SuperheroesController(INotificationHandler<Notification> notificador,
                          IApiFacade apiFacade) : base(notificador)
    {
        _apiFacade = apiFacade;
    }

    #endregion

    #region Views

    public IActionResult Index() => View();

    #endregion

    #region Method

    [HttpPost]
    public async Task<IActionResult> ListSuperheroesByFilters([FromBody] HeroFilterDTO requestFilter)
    {
        var response = await _apiFacade.SuperheroesApp.ListSuperheroesByFilters(requestFilter);
        return Ok(response);
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetByCode(int code)
    {
        var response = await _apiFacade.SuperheroesApp.GetSuperheroByCodeAsync(code);
        return Ok(response);
    }

    #endregion
}
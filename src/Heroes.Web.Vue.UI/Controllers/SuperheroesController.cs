using Heroes.Common.Util.Messages;
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

    [HttpGet("{code}")]
    public async Task<IActionResult> SaveRemoveSuperhero(int code)
    {
        string Mensagem;
        bool Success;

        var response = await _apiFacade.SuperheroesApp.RemoveSuperhero(code);

        if (!response.Success)
        {
            Success = false;
            Mensagem = ValidationMessages.MSG_FAILED("Exclusão do Super-herói");
        }
        else
        {
            Success = true;
            Mensagem = ValidationMessages.MSG_SUCCESSFUL("Exclusão");
        }

        return Json(new { success = Success, mensagem = Mensagem });
    }

    #endregion
}
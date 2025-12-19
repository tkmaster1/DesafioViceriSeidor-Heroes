using Heroes.Common.Util.Response;
using Heroes.Common.Util.Services;
using Microsoft.AspNetCore.Mvc;

namespace Heroes.Web.Vue.UI.Controllers;

public abstract class MainController : Controller
{
    #region Properties

    private readonly NotificationHandler _notificador;

    #endregion

    #region Constructor

    protected MainController(INotificationHandler<Notification> notificador)
    {
        _notificador = (NotificationHandler)notificador;        
    }

    #endregion

    #region Method

    protected ActionResult CustomResponse(object result = null, bool error = false, string message = null)
    {
        if (error)
        {
            return NotFound(new ResponseFailure
            {
                Success = false,
                Errors = new List<string> { message }
            });
        }

        return OperacaoValida()
            ? Ok(new ResponseSuccess<object> { Success = true, Data = result })
            : Conflict(new ResponseFailure { Success = false, Errors = (IEnumerable<string>)_notificador.GetNotifications() });
    }

    protected bool OperacaoValida() => !_notificador.HasNotifications;

    #endregion
}
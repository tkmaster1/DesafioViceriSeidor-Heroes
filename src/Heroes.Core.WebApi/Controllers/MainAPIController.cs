using Heroes.Common.Util.Response;
using Heroes.Common.Util.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Heroes.Core.WebApi.Controllers;

[ExcludeFromCodeCoverage]
[ApiController]
public abstract class MainAPIController : ControllerBase
{
    #region Properties

    private readonly NotificationHandler _notificador;

    #endregion

    #region Constructor

    protected MainAPIController(INotificationHandler<Notification> notificador)
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

        return ValidOperation()
            ? Ok(new ResponseSuccess<object> { Success = true, Data = result })
            : Conflict(new ResponseFailure { Success = false, Errors = (IEnumerable<string>)_notificador.GetNotifications() });
    }

    protected bool ValidOperation() => !_notificador.HasNotifications;

    #endregion
}

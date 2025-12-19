using Heroes.Common.Util.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Heroes.Web.Vue.UI.Configurations.Filters;

public class CustomActionFilterConfiguration : ActionFilterAttribute, IActionFilter, IOrderedFilter
{
    #region Properties

    // public int Order => int.MaxValue - 10;
    protected readonly NotificationHandler _notifications;
    private readonly ILogger<CustomActionFilterConfiguration> _logger;
    private readonly ILoggerFactory _loggerFactory;

    #endregion

    #region Constructor

    public CustomActionFilterConfiguration(INotificationHandler<Notification> notifications,
        ILogger<CustomActionFilterConfiguration> logger,
        ILoggerFactory loggerFactory)
    {
        _notifications = (NotificationHandler)notifications;
        _logger = logger;
        _loggerFactory = loggerFactory;
    }

    #endregion

    #region Methods

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        // verifica se tem notificação de erro na api
        if (_notifications.HasNotifications)
        {
            _logger.LogError("[" + DateTime.Now.ToString() + "] " + "[ERROR] - [Sistema] " + _notifications.GetNotifications().Select(x => x.Message).FirstOrDefault());

            context.HttpContext.Response.StatusCode = 400;
            context.HttpContext.Response.Headers.Append("warning-Notification", _notifications.GetNotifications().Select(x => x.Message).FirstOrDefault());
        }

        if (context.Exception != null)
        {
            switch (context.Exception)
            {
                default:
                    _logger.LogError($"[{DateTime.Now.ToString()}] [ERROR] - [Sistema]{context.Exception?.Message}" ?? context.Exception?.StackTrace ?? "");
                    break;
            }

            _notifications.Handle(new Notification("500", message: context.Exception.Message ?? context.Exception.StackTrace));

            var objErros = new
            {
                key = "Error",
                value = context.Exception.Message
            };

            context.Result = new ObjectResult(new
            {
                Success = false,
                Errors = objErros
            })
            {
                StatusCode = 500
            };

            context.ExceptionHandled = true;
        }

        base.OnActionExecuted(context);
    }

    public override void OnActionExecuting(ActionExecutingContext context) => base.OnActionExecuting(context);

    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
    }

    #endregion
}
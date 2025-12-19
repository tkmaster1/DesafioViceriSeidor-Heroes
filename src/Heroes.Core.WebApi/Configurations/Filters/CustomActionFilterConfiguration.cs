using Heroes.Common.Util.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;

namespace Heroes.Core.WebApi.Configurations.Filters;

public class CustomActionFilterConfiguration : IActionFilter, IOrderedFilter
{
    #region Properties

    public int Order => int.MaxValue - 10;
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

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception != null)
        {
            switch (context.Exception)
            {
                case SqlException:
                    _logger.LogError($"[{DateTime.Now.ToString()}] [ERROR] - [Database]{context.Exception?.Message}" ?? context.Exception?.StackTrace ?? "");
                    break;
                default:
                    _logger.LogError($"[{DateTime.Now.ToString()}] [ERROR] - [Sistema]{context.Exception?.Message}" ?? context.Exception?.StackTrace ?? "");
                    break;
            }

            _notifications.Handle(new Notification("500", context.Exception.Message ?? context.Exception.StackTrace));

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
    }

    public void OnActionExecuting(ActionExecutingContext context) { }

    #endregion
}
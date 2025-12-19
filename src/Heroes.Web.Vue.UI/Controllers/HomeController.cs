using Heroes.Common.Util.Services;
using Microsoft.AspNetCore.Mvc;

namespace Heroes.Web.Vue.UI.Controllers;

public class HomeController : MainController
{
    #region Constructor

    public HomeController(INotificationHandler<Notification> notificador)
        : base(notificador)
    {
    }

    #endregion

    #region Views

    public IActionResult Index() => View();

    //public IActionResult Privacy()
    //{
    //    return View();
    //}    

    #endregion
}

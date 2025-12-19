using Heroes.Common.Util.Http;
using Heroes.Common.Util.Services;
using Heroes.Core.Application.Facades;
using Heroes.Core.Application.Facades.Interfaces;
using Heroes.Core.Application.Services.Web;
using Heroes.Core.Application.Services.Web.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Heroes.Web.Vue.UI.Configurations;

public static class DependencyInjectionConfiguration
{
    public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        // Lifestyle.Transient => Uma instância para cada solicitação
        // Lifestyle.Singleton => Uma instância única para a classe (para servidor)
        // Lifestyle.Scoped => Uma instância única para o request

        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddScoped<INotificationHandler<Notification>, NotificationHandler>();
        //services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IBaseService, BaseHttpService>();
        services.AddScoped<IBaseServiceResolver, BaseServiceResolver>();

        // 2) Facade
        services.AddScoped<IApiFacade, ApiFacade>();
    }
}
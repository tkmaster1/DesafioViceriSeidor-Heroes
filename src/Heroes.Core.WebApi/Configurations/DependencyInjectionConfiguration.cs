using Heroes.Common.Util.Services;
using Heroes.Core.Application.Facades;
using Heroes.Core.Application.Facades.Interfaces;
using Heroes.Core.Application.Services;
using Heroes.Core.Data.Context;
using Heroes.Core.Data.Repositories;
using Heroes.Core.Domain.Interfaces.Repositories;
using Heroes.Core.Domain.Interfaces.Services;

namespace Heroes.Core.WebApi.Configurations;

public static class DependencyInjectionConfiguration
{
    public static void AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        // Lifestyle.Transient => Uma instância para cada solicitação
        // Lifestyle.Singleton => Uma instância única para a classe (para servidor)
        // Lifestyle.Scoped => Uma instância única para o request

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<INotificationHandler<Notification>, NotificationHandler>();

        #region Application - Facade

        services.AddTransient<IHeroFacade, HeroFacade>();
        services.AddTransient<ISuperpowerFacade, SuperpowerFacade>();
        //services.AddScoped<IBookFacade, BookFacade>();

        //services.AddTransient<IGenreFacade, GenreFacade>();
        //services.AddTransient<IPublisherFacade, PublisherFacade>();

        #endregion

        #region Domain

        services.AddTransient<IHeroAppService, HeroAppService>();
        services.AddTransient<ISuperpowerAppService, SuperpowerAppService>();
        //services.AddScoped<IBookAppService, BookAppService>();

        //services.AddTransient<IGenreAppService, GenreAppService>();
        //services.AddTransient<IPublisherAppService, PublisherAppService>();

        #endregion

        #region InfraData

        services.AddScoped<AppDbContext>();

        services.AddTransient<IHeroRepository, HeroRepository>();
        services.AddTransient<ISuperpowerRepository, SuperpowerRepository>();
        //services.AddScoped<IBookRepository, BookRepository>();

        //services.AddTransient<IGenreRepository, GenreRepository>();
        //services.AddTransient<IPublisherRepository, PublisherRepository>();

        #endregion
    }
}
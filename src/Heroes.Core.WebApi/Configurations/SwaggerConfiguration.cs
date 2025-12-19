using Microsoft.OpenApi.Models;

namespace Heroes.Core.WebApi.Configurations;

public static class SwaggerConfiguration
{
    public static void AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddMvcCore().AddApiExplorer();
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc
            (
                "v1"
                , new OpenApiInfo
                {
                    Version = configuration["AppSettings:Application:Version"],
                    Title = configuration["AppSettings:Application:Title"],
                    Description = configuration["AppSettings:Application:Description"],
                    Contact = new OpenApiContact
                    {
                        Name = configuration["AppSettings:Enterprise:Name"],
                        Email = configuration["AppSettings:Enterprise:Email"]
                    }
                }
            );

            s.CustomSchemaIds(x => x.FullName.Replace("+", "_"));
            s.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        });
    }

    /// <summary>
    /// Método de chamada no app builder do Swagger Configuration 
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catálogo de super-heróis - Core API");
            c.RoutePrefix = "";
        });

        return app;
    }
}
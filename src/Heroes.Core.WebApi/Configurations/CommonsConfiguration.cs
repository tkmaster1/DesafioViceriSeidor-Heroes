using Heroes.Core.WebApi.Configurations.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.IO.Compression;
using System.Text.Json.Serialization;

namespace Heroes.Core.WebApi.Configurations;

public static class CommonsConfiguration
{
    public static IServiceCollection AddApiCommonsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(CustomActionFilterConfiguration));

        }).AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.WriteIndented = true;
        });

        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = false;
        });

        services.AddHttpContextAccessor();
        services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddResponseCaching();

        services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);

        services.AddResponseCompression(options =>
        {
            options.Providers.Add<GzipCompressionProvider>();
            options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "image/svg+xml" });
        });

        services.AddDistributedMemoryCache();

     //   services.AddScoped<JwtBearerEventsHandler>();

        services.AddCors();

        services.AddMvc(options =>
        {
            options.SuppressAsyncSuffixInActionNames = false;
        });

      //  IdentityModelEventSource.ShowPII = true;

        var provider = services.BuildServiceProvider();

        return services;
    }

    public static IApplicationBuilder UseApiCommonsConfiguration(this IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        if (env.IsDevelopment() || env.IsStaging())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catálogo de super-heróis - Core API"));
        }
        else
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseCookiePolicy();

        app.UseCors
         (
             c =>
             {
                 c.AllowAnyOrigin();
                 c.AllowAnyHeader();
                 c.AllowAnyMethod();
             }
         );

      //  app.UseAuthenticationConfiguration();

        app.UseLoggingConfiguration(loggerFactory);

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseStaticFiles();

        app.UseGlobalizationConfiguration();

        return app;
    }
}
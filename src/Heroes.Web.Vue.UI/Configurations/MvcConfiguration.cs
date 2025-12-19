using Heroes.Common.Util.Http;
using Heroes.Common.Util.Services;
using Heroes.Core.Application.Services.Web;
using Heroes.Core.Application.Services.Web.Interfaces;
using Heroes.Web.Vue.UI.Configurations.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.IO.Compression;

namespace Heroes.Web.Vue.UI.Configurations;

public static class MvcConfiguration
{
    public static IServiceCollection AddMvcConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddControllers(x =>
        {
            x.Filters.Add(typeof(CustomActionFilterConfiguration));
            x.ModelMetadataDetailsProviders.Add(new SystemTextJsonValidationMetadataProvider());
        });

        services.AddControllersWithViews();

        // Adicionando suporte a componentes Razor (ex: Telas do Identity)
        services.AddRazorPages();
        
        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = false;
        });

        services.AddAntiforgery(options =>
        {
            options.FormFieldName = "AntiforgeryFieldname";
            options.HeaderName = "X-XSRF-TOKEN";
            options.SuppressXFrameOptionsHeader = false;
        });

        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.AddHttpContextAccessor();
        services.AddHttpClient();

        //aumenta o limite do corpo da requisição multipart 
        services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = long.MaxValue;
            options.ValueLengthLimit = int.MaxValue;
        });

        services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);

        services.AddResponseCompression(options =>
        {
            options.Providers.Add<GzipCompressionProvider>();
        });
        
        var provider = services.BuildServiceProvider();

        #region HTTP

        //services.AddTransient<AuthHeaderHandler>();

        //// Registro dos serviços comuns de HTTP
        //services.AddCommonHttpBase(configuration);
        services.AddScoped<IBaseService, BaseHttpService>();
        services.AddScoped<IBaseServiceResolver, BaseServiceResolver>();

        #endregion

        services.AddCors();

        return services;
    }
}
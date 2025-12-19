using Heroes.Common.Util.Configuration;
using Microsoft.Extensions.FileProviders;

namespace Heroes.Web.Vue.UI.Configurations;

public static class LoggerConfiguration
{
    public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        loggerFactory.UseLoggerFactory(Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Log\\"),
            $"Heroes_web_vue_{DateTime.Now.ToString("yyyyMMdd")}.txt");

        loggerFactory.CreateLogger("Heroes-web-vue").LogError("init");

        app.UseFileServer(new FileServerOptions()
        {
            FileProvider = new PhysicalFileProvider(
         Path.Combine(Directory.GetCurrentDirectory(), @"Log")),
            RequestPath = new PathString("/app-log"),
            EnableDirectoryBrowsing = true,
            EnableDefaultFiles = true
        });

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Log")),
            RequestPath = new PathString("/app-log"),
        });

        return app;
    }
}
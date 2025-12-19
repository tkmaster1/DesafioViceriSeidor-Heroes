namespace Heroes.Web.Vue.UI.Configurations;

public static class CommonsConfiguration
{
    public static IApplicationBuilder UseCommonsConfiguration(this IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        if (env.IsDevelopment() || env.IsProduction() || env.IsStaging())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error/500");
            app.UseStatusCodePagesWithRedirects("/Home/Error/{0}");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

     //   app.UseAuthenticationConfiguration();

        app.UseLoggingConfiguration(loggerFactory);

        app.UseGlobalizationConfiguration();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
              // pattern: "{controller=Home}/{action=Default}/{id?}");

            // Mapeando componentes Razor Pages (ex: Identity)
            endpoints.MapRazorPages();
        });

        return app;
    }
}

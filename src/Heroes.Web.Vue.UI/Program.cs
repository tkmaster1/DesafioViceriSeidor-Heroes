using Heroes.Web.Vue.UI.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
       .SetBasePath(builder.Environment.ContentRootPath)
       .AddJsonFile("appsettings.json", true, true)
       .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
       .AddEnvironmentVariables();

#region ConfigureServices

// Extension Method de resolução de DI (DependencyInjectionConfig)
builder.Services.AddDependencyInjectionConfiguration();

builder.Services.AddMvcConfiguration(builder.Configuration);

// Inicio AutoMapper
builder.Services.AddAutoMapperConfiguration();

//// Extension Method de configuração do Identity
//builder.Services.AddAuthenticationConfiguration(builder.Configuration);

#endregion

#region Configure

await using var app = builder.Build();
var loggerFactory = app.Services.GetService<ILoggerFactory>();

app.UseCommonsConfiguration(app.Environment, loggerFactory);

//app.UseResponseApi();

await app.RunAsync();

#endregion


//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();

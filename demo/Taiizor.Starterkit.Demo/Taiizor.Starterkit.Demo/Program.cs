using Taiizor.Starterkit.Extension;
using Taiizor.Starterkit.Interface;

namespace Taiizor.Starterkit.Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Set theme to the configuration
            //IConfiguration themeConfiguration = new ConfigurationBuilder()
            //    .AddJsonFile("DemoSettings.json")
            //    .Build();

            //ThemeSettings.Init(themeConfiguration, "Demo");

            // Set icons to the configuration
            //IConfiguration iconConfiguration = new ConfigurationBuilder()
            //    .AddJsonFile("DemoIcons.json")
            //    .Build();

            //ThemeIcons.Init(iconConfiguration, "Demo");

            ThemeSettings.Init("DemoSettings.json", "Demo");
            ThemeIcons.Init("DemoIcons.json", "Demo");

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<ITheme, Theme>();
            builder.Services.AddSingleton<IBootstrapBase, BootstrapBase>();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler(ThemeSettings.Config.ExceptionHandler);
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage(ThemeSettings.Config.MapFallbackPage);

            app.Run();
        }
    }
}
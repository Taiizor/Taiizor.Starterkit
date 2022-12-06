using Taiizor.Starterkit.Extension;
using Taiizor.Starterkit.Interface;

namespace Taiizor.Starterkit.Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<ITheme, Theme>();
            builder.Services.AddSingleton<IBootstrapBase, BootstrapBase>();

            WebApplication app = builder.Build();

            // Set settings to the configuration
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("DemoSettings.json")
                .Build();

            ThemeSettings.Init(configuration, "Demo");

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
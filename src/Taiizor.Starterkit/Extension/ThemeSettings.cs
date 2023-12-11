using Microsoft.Extensions.Configuration;

namespace Taiizor.Starterkit.Extension
{
    public class ThemeSettings
    {
        public static ThemeBase Config = new();

        public static void Init(string Path, string Key)
        {
            IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile(Path)
                .Build();

            Init(Configuration, Key);
        }

        public static void Init(IConfiguration Configuration, string Key)
        {
            Config = Configuration.GetSection(Key).Get<ThemeBase>() ?? Config;

            if (Config.DevelopmentMode)
            {
                Config.Domain = "https://localhost/";

                Config.TempData.CookieDomain = "localhost";
                Config.Languages.CookieDomain = "localhost";
                Config.Application.CookieDomain = "localhost";
                Config.Antiforgery.CookieDomain = "localhost";
                Config.Authenticator.CookieDomain = "localhost";
                Config.Authentication.CookieDomain = "localhost";
            }
        }
    }
}
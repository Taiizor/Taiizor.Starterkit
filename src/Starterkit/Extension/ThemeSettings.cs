using Microsoft.Extensions.Configuration;

namespace Starterkit.Extension
{
    public class ThemeSettings
    {
        public static ThemeBase Config;

        public static void Init(IConfiguration configuration, string key)
        {
            Config = configuration.GetSection(key).Get<ThemeBase>();
        }
    }
}
namespace Taiizor.Starterkit.Extension
{
    // Base type class for theme settings
    public class ThemeBase
    {
        public int Major { get; set; }

        public int Minor { get; set; }

        public int Build { get; set; }

        public string Uri { get; set; }

        public string Domain { get; set; }

        public string SvgDir { get; set; }

        public bool Preloader { get; set; }

        public string PagesDir { get; set; }

        public string AssetsDir { get; set; }

        public string Direction { get; set; }

        public string LayoutDir { get; set; }

        public string IconsType { get; set; }

        public string PartialsDir { get; set; }

        public string ModeDefault { get; set; }

        public ThemeAssets Assets { get; set; }

        public string LocaleDefault { get; set; }

        public bool ModeSwitchEnabled { get; set; }

        public string MapFallbackPage { get; set; }

        public string ExceptionHandler { get; set; }

        public bool LocaleSwitchEnabled { get; set; }

        public ThemeLanguages Languages { get; set; }

        public ThemeThirdParty ThirdParty { get; set; }

        public ThemeCompression Compression { get; set; }

        public ThemeSocialMedia SocialMedia { get; set; }

        public Dictionary<string, ThemeVendors> Vendors { get; set; }
    }
}
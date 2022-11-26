namespace Starterkit.Extension
{
    // Base type class for theme settings
    public class ThemeBase
    {
        public SortedDictionary<string, SortedDictionary<string, string[]>> Vendors { get; set; }

        public ThemeThirdParty ThirdParty { get; set; }

        public ThemeLanguages Languages { get; set; }

        public bool LocaleSwitchEnabled { get; set; }

        public bool ModeSwitchEnabled { get; set; }

        public string LocaleDefault { get; set; }

        public ThemeAssets Assets { get; set; }

        public string ModeDefault { get; set; }

        public string LayoutDir { get; set; }

        public string Direction { get; set; }

        public string AssetsDir { get; set; }

        public string Domain { get; set; }

        public string Uri { get; set; }
    }
}
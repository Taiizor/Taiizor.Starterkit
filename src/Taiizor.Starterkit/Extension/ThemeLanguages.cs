namespace Taiizor.Starterkit.Extension
{
    public class ThemeLanguages
    {
        public int CookieExpires { get; set; }

        public string CookieName { get; set; }

        public string DefaultFlag { get; set; }

        public string UnknownName { get; set; }

        public string UnknownLang { get; set; }

        public string CookieDomain { get; set; }

        public List<string> ActiveLang { get; set; }

        public Dictionary<string, Dictionary<string, string>> List { get; set; }
    }
}
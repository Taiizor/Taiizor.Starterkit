namespace Taiizor.Starterkit.Extension
{
    public class ThemeAuthenticator
    {
        public int CookieExpires { get; set; }

        public string CookieName { get; set; }

        public string ProviderName { get; set; }

        public string CookieDomain { get; set; }

        public int CookieValueLength { get; set; }

        public int ProviderKeyLength { get; set; }

        public string ProviderDisplay { get; set; }

        public string ProviderKeySplit { get; set; }

        public string CookieValueSplit { get; set; }
    }
}
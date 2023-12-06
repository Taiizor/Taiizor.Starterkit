namespace Taiizor.Starterkit.Extension
{
    public class ThemeAntiforgery
    {
        public bool SuppressX { get; set; }

        public int CookieExpires { get; set; }

        public string CookieName { get; set; }

        public string HeaderName { get; set; }

        public string CookieDomain { get; set; }

        public string FormFieldName { get; set; }
    }
}
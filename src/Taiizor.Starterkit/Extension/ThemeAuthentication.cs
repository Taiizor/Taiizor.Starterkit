namespace Taiizor.Starterkit.Extension
{
    public class ThemeAuthentication
    {
        public int CookieExpires { get; set; }

        public string CookieName { get; set; }

        public string CookieDomain { get; set; }

        public ThemeTwoFactorUserId TwoFactorUserId { get; set; }

        public ThemeTwoFactorRemember TwoFactorRemember { get; set; }
    }
}
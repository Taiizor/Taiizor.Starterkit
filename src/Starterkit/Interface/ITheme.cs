using Starterkit.Enum;

namespace Starterkit.Interface
{
    // Core theme interface
    public interface ITheme
    {
        void AddHtmlAttribute(string scope, string attributeName, string attributeValue);

        void AddHtmlClass(string scope, string className);

        string PrintHtmlAttributes(string scope);

        string PrintHtmlClasses(string scope);

        string GetSvgIcon(string path, string classNames);

        void SetDomain(string flag);

        string GetDomain();

        void SetUri(string flag);

        string GetUri();

        void SetLocaleSwitch(bool flag);

        bool IsLocaleSwitchEnabled();

        void SetLocaleDefault(string flag);

        string GetLocaleDefault();

        string GetLocaleDefaultLower();

        string GetLocaleDefaultReplace();

        void SetModeSwitch(bool flag);

        bool IsModeSwitchEnabled();

        void SetModeDefault(string flag);

        string GetModeDefault();

        void SetDirection(DirectionEnum direction);

        DirectionEnum GetDirection();

        bool IsRtlDirection();

        string GetAssetPath(string path);

        string GetPartials(string path);

        string GetLayout(string path);

        string GetPages(string path);

        string ExtendCssFilename(string path);

        Dictionary<string, Dictionary<string, string>> GetLanguages();

        KeyValuePair<string, string> GetLanguages(string lang);

        List<string> GetLangActiveLang();

        string GetLangUnknownName();

        string GetLangUnknownLang();

        string GetLangDefaultFlag();

        string GetLangCookieName();

        bool IsLangLocaleDefault(string lang);

        bool IsLangActiveLang(string lang);

        string GetManifest();

        string GetFavicon();

        string[] GetFonts();

        string[] GetGlobalAssets(TypeEnum type);

        void AddVendors(string[] vendors);

        void AddVendor(string vendor);

        void AddJavascriptFile(string file);

        void AddFontFile(string file);

        void AddCssFile(string file);

        string[] GetJavascriptFiles();

        string[] GetFontFiles();

        string[] GetCssFiles();

        string[] GetVendors(string type);

        string GetAttributeValue(string scope, string attributeName);
    }
}
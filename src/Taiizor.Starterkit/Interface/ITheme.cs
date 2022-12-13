using Microsoft.AspNetCore.Http.Features;
using System.IO.Compression;
using Taiizor.Starterkit.Enum;
using Taiizor.Starterkit.Extension;

namespace Taiizor.Starterkit.Interface
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

        void SetExceptionHandler(string flag);

        string GetExceptionHandler();

        void SetMapFallbackPage(string flag);

        string GetMapFallbackPage();

        void SetVersion(string flag);

        string GetVersion();

        void SetUri(string flag);

        string GetUri();

        void SetLocaleSwitch(bool flag);

        bool IsLocaleSwitchEnabled();

        void SetLocale(string flag);

        string GetLocale();

        string GetLocaleLower();

        string GetLocaleReplace();

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

        Dictionary<string, Dictionary<string, string>> GetSocialMedias();

        string GetSocialMedia(string name, MediaEnum type);

        ThemeCompression GetCompression();

        ThemeCompressionLevel GetCompressionLevel();

        CompressionLevel GetCompressionLevelGzip();

        CompressionLevel GetCompressionLevelBrotli();

        CompressionLevel GetCompressionLevelDeflate();

        ThemeCompressionResponse GetCompressionResponse();

        bool GetCompressionResponseEnableHttps();

        string[] GetCompressionResponseMimeTypes();

        ThemeCompressionStaticFile GetCompressionStaticFile();

        HttpsCompressionMode GetCompressionStaticFileMode();

        TimeSpan GetCompressionStaticFileMaxAge();

        string[] GetCompressionStaticFileHeaders();

        string[] GetCompressionStaticFileExtensions();

        Dictionary<string, Dictionary<string, string>> GetLanguages();

        KeyValuePair<string, string> GetLanguages(string lang);

        List<string> GetLangActiveLang();

        string GetLangUnknownName();

        string GetLangUnknownLang();

        string GetLangDefaultFlag();

        string GetLangCookieName();

        bool IsLangLocale(string lang);

        bool IsLangLocaleDefault(string lang);

        bool IsLangActiveLang(string lang);

        string GetManifest();

        string GetFavicon();

        string[] GetJavascripts();

        string[] GetFonts();

        string[] GetCss();

        string[] GetGlobalAssets(TypeEnum type);

        void AddVendors(string[] vendors);

        void AddVendor(string vendor);

        void AddJavascriptFile(string file);

        void AddFontFile(string file);

        void AddCssFile(string file);

        string[] GetJavascriptFiles();

        string[] GetFontFiles();

        string[] GetCssFiles();

        void ClearJavascriptFiles();

        void ClearFontFiles();

        void ClearCssFiles();

        string[] GetVendors(TypeEnum type);

        string GetAttributeValue(string scope, string attributeName);
    }
}
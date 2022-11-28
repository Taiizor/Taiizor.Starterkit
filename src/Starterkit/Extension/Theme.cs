using Starterkit.Interface;
using System.Globalization;

namespace Starterkit.Extension
{
    // Core theme class
    public class Theme : ITheme
    {
        // Theme variables
        private string _direction = "ltr";

        private string _modeDefault = "system";

        private bool _modeSwitchEnabled = true;

        private bool _localeSwitchEnabled = true;

        private readonly SortedDictionary<string, string[]> _htmlClasses = new();

        private readonly SortedDictionary<string, SortedDictionary<string, string>> _htmlAttributes = new();

        // Add HTML attributes by scope
        public void AddHtmlAttribute(string scope, string attributeName, string attributeValue)
        {
            SortedDictionary<string, string> attribute = new();

            if (_htmlAttributes.ContainsKey(scope))
            {
                attribute = _htmlAttributes[scope];
            }

            attribute[attributeName] = attributeValue;

            _htmlAttributes[scope] = attribute;
        }

        // Add HTML class by scope
        public void AddHtmlClass(string scope, string className)
        {
            List<string> list = new();

            if (_htmlClasses.TryGetValue(scope, out string[] value))
            {
                list = value.ToList();
            }

            list.Add(className);

            _htmlClasses[scope] = list.ToArray();
        }

        // Print HTML attributes for the HTML template
        public string PrintHtmlAttributes(string scope)
        {
            List<string> list = new();

            if (_htmlAttributes.TryGetValue(scope, out SortedDictionary<string, string> value))
            {
                foreach (KeyValuePair<string, string> attribute in value)
                {
                    string item = attribute.Key + "=\"" + attribute.Value + "\"";
                    list.Add(item);
                }

                return string.Join(" ", list);
            }

            return null;
        }

        // Print HTML classes for the HTML template
        public string PrintHtmlClasses(string scope)
        {
            if (_htmlClasses.TryGetValue(scope, out string[] value))
            {
                return string.Join(" ", value);
            }

            return null;
        }

        // Get SVG icon content
        public string GetSvgIcon(string path, string classNames)
        {
            string svg = File.ReadAllText($"./wwwroot/assets/media/icons/{path}");

            return $"<span class=\"{classNames}\">{svg}</span>";
        }

        // Set the domain to baseuri
        public void SetDomain(string flag)
        {
            ThemeSettings.Config.Domain = flag;
        }

        // Get current domain
        public string GetDomain()
        {
            return ThemeSettings.Config.Domain;
        }

        // Set the domain to uri
        public void SetUri(string flag)
        {
            ThemeSettings.Config.Uri = flag;
        }

        // Get current uri
        public string GetUri()
        {
            return ThemeSettings.Config.Uri;
        }

        // Set locale mode enabled status
        public void SetLocaleSwitch(bool flag)
        {
            _localeSwitchEnabled = flag;
        }

        // Check locale mode status
        public bool IsLocaleSwitchEnabled()
        {
            return _localeSwitchEnabled;
        }

        // Set the locale to language
        public void SetLocaleDefault(string flag)
        {
            if (GetLocaleDefault() != flag)
            {
                CultureInfo.CurrentCulture = new(flag);
                CultureInfo.CurrentUICulture = new(flag);
            }
        }

        // Get current locale
        public string GetLocaleDefault()
        {
            return CultureInfo.CurrentCulture.Name;
        }

        // Get current locale lower
        public string GetLocaleDefaultLower()
        {
            return GetLocaleDefault().ToLowerInvariant();
        }

        // Get current locale replace
        public string GetLocaleDefaultReplace()
        {
            return GetLocaleDefault().Replace("-", "_");
        }

        // Set dark mode enabled status
        public void SetModeSwitch(bool flag)
        {
            _modeSwitchEnabled = flag;
        }

        // Check dark mode status
        public bool IsModeSwitchEnabled()
        {
            return _modeSwitchEnabled;
        }

        // Set the mode to dark or light
        public void SetModeDefault(string flag)
        {
            _modeDefault = flag;
        }

        // Get current mode
        public string GetModeDefault()
        {
            return _modeDefault;
        }

        // Set style direction
        public void SetDirection(string direction)
        {
            _direction = direction;
        }

        // Get style direction
        public string GetDirection()
        {
            return _direction;
        }

        // Check if style direction is RTL
        public bool IsRtlDirection()
        {
            return _direction.ToLower() == "rtl";
        }

        // Get assets path
        public string GetAssetPath(string path)
        {
            return $"/{ThemeSettings.Config.AssetsDir}{path}";
        }

        // Extend CSS file name with RTL
        public string ExtendCssFilename(string path)
        {

            if (IsRtlDirection())
            {
                path = path.Replace(".css", ".rtl.css");
            }

            return path;
        }

        // Include favicon from settings
        public string GetManifest()
        {
            return GetAssetPath(ThemeSettings.Config.Assets.Manifest);
        }

        // Include favicon from settings
        public string GetFavicon()
        {
            return GetAssetPath(ThemeSettings.Config.Assets.Favicon);
        }

        // Include the fonts from settings
        public string[] GetFonts()
        {
            return ThemeSettings.Config.Assets.Fonts.ToArray();
        }

        // Get the languages
        public Dictionary<string, Dictionary<string, string>> GetLanguages()
        {
            return ThemeSettings.Config.Languages.List;
        }

        // Get the languages value
        public KeyValuePair<string, string> GetLanguages(string lang)
        {
            if (GetLanguages().TryGetValue(lang, out Dictionary<string, string> value))
            {
                return value.First();
            }
            else
            {
                return new(ThemeSettings.Config.Languages.UnknownName, ThemeSettings.Config.Languages.DefaultFlag);
            }
        }

        // Get the languages active lang
        public List<string> GetLangActiveLang()
        {
            return ThemeSettings.Config.Languages.ActiveLang;
        }

        // Get the languages unknown name
        public string GetLangUnknownName()
        {
            return ThemeSettings.Config.Languages.UnknownName;
        }

        // Get the languages unknown lang
        public string GetLangUnknownLang()
        {
            return ThemeSettings.Config.Languages.UnknownLang;
        }

        // Get the languages default flag
        public string GetLangDefaultFlag()
        {
            return ThemeSettings.Config.Languages.DefaultFlag;
        }

        // Get the languages cookie name
        public string GetLangCookieName()
        {
            return ThemeSettings.Config.Languages.CookieName;
        }

        // Check lang locale default status
        public bool IsLangLocaleDefault(string lang)
        {
            if (GetLanguages(lang).Key == GetLocaleDefault())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // Check lang active lang status
        public bool IsLangActiveLang(string lang)
        {
            if (GetLanguages().ContainsKey(lang))
            {
                KeyValuePair<string, string> pair = GetLanguages(lang);

                if (GetLangActiveLang().Contains(pair.Key))
                {
                    return true;
                }
            }

            return false;
        }

        // Get the global assets
        public string[] GetGlobalAssets(string type)
        {
            List<string> files = type == "Css" ? ThemeSettings.Config.Assets.Css : ThemeSettings.Config.Assets.Js;

            List<string> newList = new();

            foreach (string file in files)
            {
                if (type == "Css")
                {
                    newList.Add(GetAssetPath(ExtendCssFilename(file)));
                }
                else
                {
                    newList.Add(GetAssetPath(file));
                }
            }

            return newList.ToArray();
        }

        // Get attributes by scope
        public string GetAttributeValue(string scope, string attributeName)
        {
            if (_htmlAttributes.TryGetValue(scope, out SortedDictionary<string, string> value))
            {
                if (value.TryGetValue(attributeName, out string valued))
                {
                    return valued;
                }

                return "";
            }

            return "";
        }
    }
}
using Starterkit.Enum;
using Starterkit.Interface;
using System.Globalization;

namespace Starterkit.Extension
{
    // Core theme class
    public class Theme : ITheme
    {
        // Theme variables
        private string _localeDefault;

        private string _modeDefault = "system";

        private bool _modeSwitchEnabled = true;

        private bool _localeSwitchEnabled = true;

        private DirectionEnum _direction = DirectionEnum.LTR;

        private readonly SortedDictionary<string, string[]> _htmlClasses = new();

        private readonly SortedDictionary<string, SortedDictionary<string, string>> _htmlAttributes = new();

        // Keep page level assets
        private readonly List<string> _jsFiles = new();

        private readonly List<string> _cssFiles = new();

        private readonly List<string> _fontFiles = new();

        private readonly List<string> _vendorFiles = new();

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
        public void SetLocale(string flag)
        {
            if (GetLocale() != flag)
            {
                CultureInfo.CurrentCulture = new(flag);
                CultureInfo.CurrentUICulture = new(flag);
            }
        }

        // Get current locale
        public string GetLocale()
        {
            return CultureInfo.CurrentCulture.Name;
        }

        // Get current locale lower
        public string GetLocaleLower()
        {
            return GetLocale().ToLowerInvariant();
        }

        // Get current locale replace
        public string GetLocaleReplace()
        {
            return GetLocale().Replace("-", "_");
        }

        // Set the locale default to language
        public void SetLocaleDefault(string flag)
        {
            _localeDefault = flag;
        }

        // Get current locale default
        public string GetLocaleDefault()
        {
            return _localeDefault;
        }

        // Get current locale default lower
        public string GetLocaleDefaultLower()
        {
            return _localeDefault.ToLowerInvariant();
        }

        // Get current locale default replace
        public string GetLocaleDefaultReplace()
        {
            return _localeDefault.Replace("-", "_");
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
        public void SetDirection(DirectionEnum direction)
        {
            _direction = direction;
        }

        // Get style direction
        public DirectionEnum GetDirection()
        {
            return _direction;
        }

        // Check if style direction is RTL
        public bool IsRtlDirection()
        {
            return _direction == DirectionEnum.RTL;
        }

        // Get assets path
        public string GetAssetPath(string path)
        {
            return $"/{ThemeSettings.Config.AssetsDir}{path}";
        }

        // Get partials path
        public string GetPartials(string path)
        {
            return $"{ThemeSettings.Config.PartialsDir}/{path}";
        }

        // Get layout path
        public string GetLayout(string path)
        {
            return $"{ThemeSettings.Config.LayoutDir}/{path}";
        }

        // Get pages path
        public string GetPages(string path)
        {
            return $"{ThemeSettings.Config.PagesDir}/{path}";
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
            return ThemeSettings.Config.Assets.Font.ToArray();
        }

        // Get the global assets
        public string[] GetGlobalAssets(TypeEnum type)
        {
            List<string> files = type == TypeEnum.Css ? ThemeSettings.Config.Assets.Css : ThemeSettings.Config.Assets.Js;

            List<string> newList = new();

            foreach (string file in files)
            {
                if (type == TypeEnum.Css)
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

        // Add multiple vendors to the page by name
        public void AddVendors(string[] vendors)
        {
            foreach (string vendor in vendors)
            {
                if (!_vendorFiles.Contains(vendor))
                {
                    _vendorFiles.Add(vendor);
                }
            }
        }

        // Add single vendor to the page by name
        public void AddVendor(string vendor)
        {
            if (!_vendorFiles.Contains(vendor))
            {
                _vendorFiles.Add(vendor);
            }
        }

        // Add custom Javascript file to the page
        public void AddJavascriptFile(string file)
        {
            if (!_jsFiles.Contains(file))
            {
                _jsFiles.Add(file);
            }
        }

        // Add custom Font file to the page
        public void AddFontFile(string file)
        {
            if (!_fontFiles.Contains(file))
            {
                _fontFiles.Add(file);
            }
        }

        // Add custom Css file to the page
        public void AddCssFile(string file)
        {
            if (!_cssFiles.Contains(file))
            {
                _cssFiles.Add(file);
            }
        }

        // Get custom Javascript file
        public string[] GetJavascriptFiles()
        {
            return _jsFiles.ToArray();
        }

        // Get custom Font file
        public string[] GetFontFiles()
        {
            return _fontFiles.ToArray();
        }

        // Get custom Css file
        public string[] GetCssFiles()
        {
            return _cssFiles.ToArray();
        }

        // Get vendor files from settings
        public string[] GetVendors(string type)
        {
            SortedDictionary<string, SortedDictionary<string, ThemeVendors>> vendors = ThemeSettings.Config.Vendors;

            List<string> files = new();

            foreach (string vendor in _vendorFiles)
            {
                if (vendors.TryGetValue(vendor, out SortedDictionary<string, ThemeVendors> value) && value.TryGetValue(type, out ThemeVendors valued))
                {
                    List<string> vendorFiles = new();

                    vendorFiles.AddRange(valued.Font);
                    vendorFiles.AddRange(valued.Css);
                    vendorFiles.AddRange(valued.Js);

                    foreach (string file in vendorFiles)
                    {
                        string vendorPath = file.Contains("https://") || file.Contains("http://") ? file : GetAssetPath(file);

                        files.Add(vendorPath);
                    }
                }
            }

            return files.ToArray();
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
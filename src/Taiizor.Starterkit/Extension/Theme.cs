using Microsoft.AspNetCore.Http.Features;
using System.Globalization;
using System.IO.Compression;
using System.Reflection;
using Taiizor.Starterkit.Enum;
using Taiizor.Starterkit.Interface;

namespace Taiizor.Starterkit.Extension
{
    // Core theme class
    public class Theme : ITheme
    {
        // Theme variables
        private string _modeDefault = "system";

        private string _localeDefault = "en-GB";

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
            string fullpath = $"./wwwroot/{ThemeSettings.Config.AssetsDir}/{ThemeSettings.Config.SvgDir}{path}";
            string svg = @"<svg xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" viewBox=""0 0 100 100""><rect width=""100"" height=""100"" fill=""#FF9900"" rx=""4"" ry=""4""/><rect width=""50"" height=""50"" fill=""#FFB13B"" rx=""4"" ry=""4""/><rect width=""50"" height=""50"" x=""50"" y=""50"" fill=""#de8500"" rx=""4"" ry=""4""/><g fill=""#ff9900""><circle cx=""50"" cy=""18.4"" r=""18.4""/><circle cx=""72.4"" cy=""27.6"" r=""18.4""/><circle cx=""81.6"" cy=""50"" r=""18.4""/><circle cx=""72.4"" cy=""72.4"" r=""18.4""/><circle cx=""50"" cy=""81.6"" r=""18.4""/><circle cx=""27.6"" cy=""72.4"" r=""18.4""/><circle cx=""18.4"" cy=""50"" r=""18.4""/><circle cx=""27.6"" cy=""27.6"" r=""18.4""/></g><path d=""M63.086 18.385c0-7.227-5.859-13.086-13.1-13.086-7.235 0-13.096 5.859-13.096 13.086-5.1-5.11-13.395-5.11-18.497 0-5.119 5.12-5.119 13.408 0 18.524-7.234 0-13.103 5.859-13.103 13.085 0 7.23 5.87 13.098 13.103 13.098-5.119 5.11-5.119 13.395 0 18.515 5.102 5.104 13.397 5.104 18.497 0 0 7.228 5.86 13.083 13.096 13.083 7.24 0 13.1-5.855 13.1-13.083 5.118 5.104 13.416 5.104 18.513 0 5.101-5.12 5.101-13.41 0-18.515 7.216 0 13.081-5.869 13.081-13.098 0-7.227-5.865-13.085-13.081-13.085 5.101-5.119 5.101-13.406 0-18.524-5.097-5.11-13.393-5.11-18.513 0z""/><path fill=""#ffffff"" d=""M55.003 23.405v14.488L65.26 27.64c0-1.812.691-3.618 2.066-5.005 2.78-2.771 7.275-2.771 10.024 0 2.771 2.766 2.771 7.255 0 10.027-1.377 1.375-3.195 2.072-5.015 2.072L62.101 44.982H76.59c1.29-1.28 3.054-2.076 5.011-2.076 3.9 0 7.078 3.179 7.078 7.087 0 3.906-3.178 7.088-7.078 7.088-1.957 0-3.721-.798-5.011-2.072H62.1l10.229 10.244c1.824 0 3.642.694 5.015 2.086 2.774 2.759 2.774 7.25 0 10.01-2.75 2.774-7.239 2.774-10.025 0-1.372-1.372-2.064-3.192-2.064-5.003L55 62.094v14.499c1.271 1.276 2.084 3.054 2.084 5.013 0 3.906-3.177 7.077-7.098 7.077-3.919 0-7.094-3.167-7.094-7.077 0-1.959.811-3.732 2.081-5.013V62.094L34.738 72.346c0 1.812-.705 3.627-2.084 5.003-2.769 2.772-7.251 2.772-10.024 0-2.775-2.764-2.775-7.253 0-10.012 1.377-1.39 3.214-2.086 5.012-2.086l10.257-10.242H23.414c-1.289 1.276-3.072 2.072-5.015 2.072-3.917 0-7.096-3.18-7.096-7.088s3.177-7.087 7.096-7.087c1.94 0 3.725.796 5.015 2.076h14.488L27.646 34.736c-1.797 0-3.632-.697-5.012-2.071-2.775-2.772-2.775-7.26 0-10.027 2.773-2.771 7.256-2.771 10.027 0 1.375 1.386 2.083 3.195 2.083 5.005l10.235 10.252V23.407c-1.27-1.287-2.082-3.053-2.082-5.023 0-3.908 3.175-7.079 7.096-7.079 3.919 0 7.097 3.168 7.097 7.079-.002 1.972-.816 3.735-2.087 5.021z""/><g><path fill=""#000000"" d=""M5.3 50h89.38v40q0 5-5 5H10.3q-5 0-5-5Z""/><path fill=""#3f3f3f"" d=""M14.657 54.211h71.394c2.908 0 5.312 2.385 5.312 5.315v17.91c-27.584-3.403-54.926-8.125-82.011-7.683V59.526c.001-2.93 2.391-5.315 5.305-5.315z""/><path fill=""#ffffff"" stroke=""#000000"" stroke-width="".5035"" d=""M18.312 72.927c-2.103-2.107-3.407-5.028-3.407-8.253 0-6.445 5.223-11.672 11.666-11.672 6.446 0 11.667 5.225 11.667 11.672h-6.832c0-2.674-2.168-4.837-4.835-4.837-2.663 0-4.838 2.163-4.838 4.837 0 1.338.549 2.536 1.415 3.42.883.874 2.101 1.405 3.423 1.405v.012c3.232 0 6.145 1.309 8.243 3.416 2.118 2.111 3.424 5.034 3.424 8.248 0 6.454-5.221 11.68-11.667 11.68-6.442 0-11.666-5.222-11.666-11.68h6.828c0 2.679 2.175 4.835 4.838 4.835 2.667 0 4.835-2.156 4.835-4.835 0-1.329-.545-2.527-1.429-3.407-.864-.88-2.082-1.418-3.406-1.418-3.23 0-6.142-1.314-8.259-3.423zM61.588 53.005l-8.244 39.849h-6.85l-8.258-39.849h6.846l4.838 23.337 4.835-23.337zM73.255 69.513h11.683v11.664c0 6.452-5.226 11.678-11.669 11.678-6.441 0-11.666-5.226-11.666-11.678V64.676h-.017C61.586 58.229 66.827 53 73.253 53c6.459 0 11.683 5.225 11.683 11.676h-6.849c0-2.674-2.152-4.837-4.834-4.837-2.647 0-4.82 2.163-4.82 4.837v16.501c0 2.675 2.173 4.837 4.82 4.837 2.682 0 4.834-2.162 4.834-4.827V76.348h-4.834l.002-6.835z""/></g></svg>";

            if (File.Exists(fullpath))
            {
                svg = File.ReadAllText(fullpath);
            }

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

        // Set the exception handler to path
        public void SetExceptionHandler(string flag)
        {
            ThemeSettings.Config.ExceptionHandler = flag;
        }

        // Get current exception handler
        public string GetExceptionHandler()
        {
            return ThemeSettings.Config.ExceptionHandler;
        }

        // Set the map fallback page to page
        public void SetMapFallbackPage(string flag)
        {
            ThemeSettings.Config.MapFallbackPage = flag;
        }

        // Get current map fallback page
        public string GetMapFallbackPage()
        {
            return ThemeSettings.Config.MapFallbackPage;
        }

        // Set the version
        public void SetVersion(string flag)
        {
            ThemeSettings.Config.Version = flag;
        }

        // Get current version
        public string GetVersion()
        {
            return ThemeSettings.Config.Version;
        }

        // Set the assets version
        public void SetAssetVersion(bool flag)
        {
            ThemeSettings.Config.Assets.Version = flag;
        }

        // Get current assets version
        public bool GetAssetVersion()
        {
            return ThemeSettings.Config.Assets.Version;
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
                CultureInfo.DefaultThreadCurrentCulture = new(flag);
                CultureInfo.DefaultThreadCurrentUICulture = new(flag);
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

        // Get the social medias
        public Dictionary<string, Dictionary<string, string>> GetSocialMedias()
        {
            Dictionary<string, Dictionary<string, string>> list = new();

            foreach (PropertyInfo properties in ThemeSettings.Config.SocialMedia.GetType().GetProperties())
            {
                if (!list.ContainsKey(properties.Name))
                {
                    object propertyValue = properties.GetValue(ThemeSettings.Config.SocialMedia);

                    if (propertyValue != null)
                    {
                        Dictionary<string, string> result = propertyValue as Dictionary<string, string>;

                        result.TryGetValue("Url", out string url);
                        result.TryGetValue("Alt", out string alt);
                        result.TryGetValue("Icon", out string icon);
                        result.TryGetValue("Line", out string line);

                        list.Add(properties.Name, new() { { "Url", url }, { "Alt", alt }, { "Icon", icon }, { "Line", line } });
                    }
                }
            }

            return list;
        }

        // Get the social media
        public string GetSocialMedia(string name, MediaEnum type)
        {
            if (GetSocialMedias().TryGetValue(name, out Dictionary<string, string> value))
            {
                if (value.TryGetValue(type.ToString(), out string valued))
                {
                    return valued;
                }
            }

            return "";
        }

        // Get the compression
        public ThemeCompression GetCompression()
        {
            return ThemeSettings.Config.Compression;
        }

        // Get the compression level
        public ThemeCompressionLevel GetCompressionLevel()
        {
            return ThemeSettings.Config.Compression.Level;
        }

        // Get the compression level gzip
        public string GetCompressionLevelGzip()
        {
            return ThemeSettings.Config.Compression.Level.Gzip;
        }

        // Get the compression level gzip type
        public CompressionLevel GetCompressionLevelGzipType()
        {
            return ThemeSettings.Config.Compression.Level.GzipType;
        }

        // Get the compression level brotli
        public string GetCompressionLevelBrotli()
        {
            return ThemeSettings.Config.Compression.Level.Brotli;
        }

        // Get the compression level brotli type
        public CompressionLevel GetCompressionLevelBrotliType()
        {
            return ThemeSettings.Config.Compression.Level.BrotliType;
        }

        // Get the compression level deflate
        public string GetCompressionLevelDeflate()
        {
            return ThemeSettings.Config.Compression.Level.Deflate;
        }

        // Get the compression level deflate type
        public CompressionLevel GetCompressionLevelDeflateType()
        {
            return ThemeSettings.Config.Compression.Level.DeflateType;
        }

        // Get the compression response
        public ThemeCompressionResponse GetCompressionResponse()
        {
            return ThemeSettings.Config.Compression.Response;
        }

        // Get the compression response enable https
        public bool GetCompressionResponseEnableHttps()
        {
            return ThemeSettings.Config.Compression.Response.EnableHttps;
        }

        // Get the compression response mime types
        public string[] GetCompressionResponseMimeTypes()
        {
            return ThemeSettings.Config.Compression.Response.MimeTypes;
        }

        // Get the compression static file
        public ThemeCompressionStaticFile GetCompressionStaticFile()
        {
            return ThemeSettings.Config.Compression.StaticFile;
        }

        // Get the compression static file mode
        public string GetCompressionStaticFileMode()
        {
            return ThemeSettings.Config.Compression.StaticFile.Mode;
        }

        // Get the compression static file mode type
        public HttpsCompressionMode GetCompressionStaticFileModeType()
        {
            return ThemeSettings.Config.Compression.StaticFile.ModeType;
        }

        // Get the compression static file change
        public bool GetCompressionStaticFileChange()
        {
            return ThemeSettings.Config.Compression.StaticFile.Change;
        }

        // Get the compression static file max age
        public string GetCompressionStaticFileMaxAge()
        {
            return ThemeSettings.Config.Compression.StaticFile.MaxAge;
        }

        // Get the compression static file max age span
        public TimeSpan GetCompressionStaticFileMaxAgeSpan()
        {
            return ThemeSettings.Config.Compression.StaticFile.MaxAgeSpan;
        }

        // Get the compression static file max age second
        public int GetCompressionStaticFileMaxAgeSecond()
        {
            return ThemeSettings.Config.Compression.StaticFile.MaxAgeSecond;
        }

        // Get the compression static file headers
        public Dictionary<string, string> GetCompressionStaticFileHeaders()
        {
            return ThemeSettings.Config.Compression.StaticFile.Headers;
        }

        // Get the compression static file extensions
        public string[] GetCompressionStaticFileExtensions()
        {
            return ThemeSettings.Config.Compression.StaticFile.Extensions;
        }

        // Get the compression static cache control
        public string GetCompressionStaticFileCacheControl()
        {
            return ThemeSettings.Config.Compression.StaticFile.CacheControl;
        }

        // Get the compression static cache control format
        public string GetCompressionStaticFileCacheControlFormat()
        {
            return ThemeSettings.Config.Compression.StaticFile.CacheControlFormat;
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

        // Check lang locale status
        public bool IsLangLocale(string lang)
        {
            if (GetLanguages(lang).Key == GetLocale())
            {
                return true;
            }
            else
            {
                return false;
            }
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

        // Include the javascripts from settings
        public string[] GetJavascripts()
        {
            return ThemeSettings.Config.Assets.Js.ToArray();
        }

        // Include the fonts from settings
        public string[] GetFonts()
        {
            return ThemeSettings.Config.Assets.Font.ToArray();
        }

        // Include the css from settings
        public string[] GetCss()
        {
            return ThemeSettings.Config.Assets.Css.ToArray();
        }

        // Get the global assets
        public string[] GetGlobalAssets(TypeEnum type)
        {
            List<string> files = new();
            List<string> newList = new();

            switch (type)
            {
                case TypeEnum.Js:
                    files = ThemeSettings.Config.Assets.Js;
                    break;
                case TypeEnum.Css:
                    files = ThemeSettings.Config.Assets.Css;
                    break;
                case TypeEnum.Font:
                    files = ThemeSettings.Config.Assets.Font;
                    break;
            }

            if (files.Any())
            {
                foreach (string file in files)
                {
                    switch (type)
                    {
                        case TypeEnum.Js:
                            newList.Add(GetAssetPath(file));
                            break;
                        case TypeEnum.Css:
                            newList.Add(GetAssetPath(ExtendCssFilename(file)));
                            break;
                        case TypeEnum.Font:
                            newList.Add(file.Contains("https://") || file.Contains("http://") ? file : GetAssetPath(file));
                            break;
                    }
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

        // Clear custom Javascript file
        public void ClearJavascriptFiles()
        {
            _jsFiles.Clear();
        }

        // Clear custom Font file
        public void ClearFontFiles()
        {
            _fontFiles.Clear();
        }

        // Clear custom Css file
        public void ClearCssFiles()
        {
            _cssFiles.Clear();
        }

        // Get vendor files from settings
        public string[] GetVendors(TypeEnum type)
        {
            Dictionary<string, ThemeVendors> vendors = ThemeSettings.Config.Vendors;

            List<string> files = new();

            if (_vendorFiles.Any())
            {
                foreach (string vendor in _vendorFiles)
                {
                    if (vendors.TryGetValue(vendor, out ThemeVendors value))
                    {
                        List<string> vendorFiles = new();

                        switch (type)
                        {
                            case TypeEnum.Js:
                                if (value.Js != null && value.Js.Any())
                                {
                                    vendorFiles.AddRange(value.Js);
                                }
                                break;
                            case TypeEnum.Css:
                                if (value.Css != null && value.Css.Any())
                                {
                                    vendorFiles.AddRange(value.Css);
                                }
                                break;
                            case TypeEnum.Font:
                                if (value.Font != null && value.Font.Any())
                                {
                                    vendorFiles.AddRange(value.Font);
                                }
                                break;
                        }

                        if (vendorFiles.Any())
                        {
                            foreach (string file in vendorFiles)
                            {
                                string vendorPath = file.Contains("https://") || file.Contains("http://") ? file : GetAssetPath(file);

                                files.Add(vendorPath);
                            }
                        }
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
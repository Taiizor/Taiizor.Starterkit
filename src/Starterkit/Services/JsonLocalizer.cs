using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Text.Json;

namespace Starterkit.Services
{
    public class JsonLocalizer : IStringLocalizer
    {
        public static string Loc;
        
        private string Name { get; }

        private string Lang { get; }

        private string ResourcesPath { get; }

        private string ResourcesExtension { get; }
        
        private IFileProvider FileProvider { get; }

        public JsonLocalizer(IFileProvider fileProvider, string resourcePath, string name, string lang, string extension)
        {
            Name = name;
            Lang = lang;
            FileProvider = fileProvider;
            ResourcesPath = resourcePath;
            ResourcesExtension = extension;
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        
        public LocalizedString this[string name]
        {
            get
            {
                return new LocalizedString(name, GetLocalizedString(name));
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                return new LocalizedString(name, string.Format(GetLocalizedString(name), arguments));
            }
        }

        private string GetLocalizedString(string name)
        {
            Dictionary<string, string> stringMap = LoadStringMap(Lang);

            Dictionary<string, string> activeMap = LoadStringMap(CultureInfo.CurrentCulture.Name);

            if (activeMap.Count > 0)
            {
                foreach (KeyValuePair<string, string> item in activeMap)
                {
                    stringMap[item.Key] = item.Value;
                }
            }
            
            return stringMap.TryGetValue(name, out string value) ? value : $"[{name}]";
        }

        private IFileInfo ResourceCheck(string path, string name, string lang, string extension)
        {
            Loc = Path.Combine(path, lang, $"{name}.{extension}");
            return FileProvider.GetFileInfo(Path.Combine(path, lang, $"{name}.{extension}"));
        }
        
        private Dictionary<string, string> LoadStringMap(string Lang)
        {
            IFileInfo fileInfo = ResourceCheck(ResourcesPath, Name, Lang, ResourcesExtension);

            if (fileInfo.Exists)
            {
                using Stream stream = fileInfo.CreateReadStream();

                return JsonSerializer.DeserializeAsync<Dictionary<string, string>>(stream).Result;
            }
            else
            {
                return new Dictionary<string, string>();
            }
        }
    }
}
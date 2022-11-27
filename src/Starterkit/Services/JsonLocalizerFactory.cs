using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Starterkit.Extension;
using Starterkit.Helper;

namespace Starterkit.Services
{
    public class JsonLocalizerFactory : IStringLocalizerFactory
    {
        private string DefaultLang { get; }

        private string ResourcesPath { get; }

        private string ResourcesExtension { get; }

        public JsonLocalizerFactory(IOptions<LocalizationOptions> options)
        {
            ResourcesExtension = "json";
            ResourcesPath = options.Value.ResourcesPath;
            DefaultLang = ThemeSettings.Config.LocaleDefault;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            EmbeddedFileProvider resources = new(resourceSource.Assembly);

            return new JsonLocalizer(resources, ResourcesPath, resourceSource.Name, DefaultLang, ResourcesExtension);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            throw new NotImplementedException();
        }
    }
}
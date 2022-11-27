using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Starterkit.Helper;

namespace Starterkit.Services
{
    public class JsonLocalizerFactory2 : IStringLocalizerFactory
    {
        private string DefaultLang { get; }

        private string ResourcesPath { get; }

        private string ResourcesExtension { get; }

        public JsonLocalizerFactory2(IOptions<JsonLocalizerOptions> options)
        {
            DefaultLang = options.Value.DefaultLang;
            ResourcesPath = options.Value.ResourcesPath;
            ResourcesExtension = options.Value.ResourcesExtension;
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
namespace Starterkit.Helper
{
    public class JsonLocalizerOptions
    {
        public JsonLocalizerOptions()
        { }

        public char Split { get; set; } = '-';
        
        public string DefaultLang { get; set; } = string.Empty;

        public string ResourcesPath { get; set; } = string.Empty;

        public string ResourcesExtension { get; set; } = string.Empty;
    }
}
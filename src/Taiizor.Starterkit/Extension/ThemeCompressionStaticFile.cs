namespace Taiizor.Starterkit.Extension
{
    public class ThemeCompressionStaticFile
    {
        public string Mode { get; set; }

        public TimeSpan MaxAge { get; set; }

        public string[] Headers { get; set; }

        public string[] Extensions { get; set; }
    }
}
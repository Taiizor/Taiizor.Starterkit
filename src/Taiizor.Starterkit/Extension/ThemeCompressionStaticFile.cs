namespace Taiizor.Starterkit.Extension
{
    public class ThemeCompressionStaticFile
    {
        public string Mode { get; set; }

        public TimeSpan MaxAge { get; set; }

        public string[] Extensions { get; set; }

        public string CacheControl { get; set; }

        public Dictionary<string, string> Headers { get; set; }

        public int MaxAgeSecond => Convert.ToInt32(MaxAge.TotalSeconds);
    }
}
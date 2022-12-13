using Microsoft.AspNetCore.Http.Features;
using Taiizor.Starterkit.Helper;

namespace Taiizor.Starterkit.Extension
{
    public class ThemeCompressionStaticFile
    {
        public string Mode { get; set; }

        public bool Change { get; set; }

        public string MaxAge { get; set; }

        public string[] Extensions { get; set; }

        public string CacheControl { get; set; }

        public TimeSpan MaxAgeSpan => TimeSpan.Parse(MaxAge);

        public Dictionary<string, string> Headers { get; set; }

        public int MaxAgeSecond => Convert.ToInt32(MaxAgeSpan.TotalSeconds);

        public string CacheControlFormat => string.Format(CacheControl, MaxAgeSecond);

        public HttpsCompressionMode ModeType => Converter.Convert(Mode, HttpsCompressionMode.Compress);
    }
}
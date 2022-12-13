using System.IO.Compression;
using Taiizor.Starterkit.Helper;

namespace Taiizor.Starterkit.Extension
{
    public class ThemeCompressionLevel
    {
        public string Gzip { get; set; }

        public string Brotli { get; set; }

        public string Deflate { get; set; }

        public CompressionLevel GzipType => Converter.Convert(Gzip, CompressionLevel.Optimal);

        public CompressionLevel BrotliType => Converter.Convert(Brotli, CompressionLevel.Optimal);

        public CompressionLevel DeflateType => Converter.Convert(Deflate, CompressionLevel.Optimal);
    }
}
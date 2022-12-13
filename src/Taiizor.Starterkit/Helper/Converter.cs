using Microsoft.AspNetCore.Http.Features;
using System.IO.Compression;
using Taiizor.Starterkit.Enum;

namespace Taiizor.Starterkit.Helper
{
    public class Converter
    {
        public static TypeEnum Convert(string Type, TypeEnum Back = TypeEnum.Css)
        {
            foreach (TypeEnum Types in (TypeEnum[])System.Enum.GetValues(typeof(TypeEnum)))
            {
                if (Check(Type, Types))
                {
                    return Types;
                }
            }

            return Back;
        }

        public static MediaEnum Convert(string Type, MediaEnum Back = MediaEnum.Url)
        {
            foreach (MediaEnum Types in (MediaEnum[])System.Enum.GetValues(typeof(MediaEnum)))
            {
                if (Check(Type, Types))
                {
                    return Types;
                }
            }

            return Back;
        }

        public static DirectionEnum Convert(string Type, DirectionEnum Back = DirectionEnum.LTR)
        {
            foreach (DirectionEnum Types in (DirectionEnum[])System.Enum.GetValues(typeof(DirectionEnum)))
            {
                if (Check(Type, Types))
                {
                    return Types;
                }
            }

            return Back;
        }

        public static CompressionLevel Convert(string Type, CompressionLevel Back = CompressionLevel.Optimal)
        {
            foreach (CompressionLevel Types in (CompressionLevel[])System.Enum.GetValues(typeof(CompressionLevel)))
            {
                if (Check(Type, Types))
                {
                    return Types;
                }
            }

            return Back;
        }

        public static HttpsCompressionMode Convert(string Type, HttpsCompressionMode Back = HttpsCompressionMode.Compress)
        {
            foreach (HttpsCompressionMode Types in (HttpsCompressionMode[])System.Enum.GetValues(typeof(HttpsCompressionMode)))
            {
                if (Check(Type, Types))
                {
                    return Types;
                }
            }

            return Back;
        }

        private static bool Check(object Text, object Type)
        {
            return Check($"{Text}", $"{Type}");
        }

        private static bool Check(string Text, string Type)
        {
            if (Text == Type || Text.ToUpper() == Type || Text.ToUpperInvariant() == Type || Text == Type.ToLower() || Text == Type.ToLowerInvariant())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
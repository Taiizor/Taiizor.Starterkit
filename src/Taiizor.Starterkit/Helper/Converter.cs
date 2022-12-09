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
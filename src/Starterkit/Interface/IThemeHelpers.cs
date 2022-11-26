namespace Starterkit.Interface
{
    public interface IThemeHelpers
    {
        void AddBodyAttribute(string attribute, string value);

        void RemoveBodyAttribute(string attribute);

        void AddBodyClass(string className);

        void RemoveBodyClass(string className);
    }
}
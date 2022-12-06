namespace Taiizor.Starterkit.Interface
{
    // Core theme helper interface
    public interface IThemeHelpers
    {
        void AddBodyClass(string className);

        void RemoveBodyClass(string className);

        void RemoveBodyAttribute(string attribute);

        void AddBodyAttribute(string attribute, string value);
    }
}
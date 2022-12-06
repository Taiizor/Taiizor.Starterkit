namespace Taiizor.Starterkit.Interface
{
    // Core bootstrap base interface
    public interface IBootstrapBase
    {
        void InitRtl();

        void InitDomain();

        void InitLayout();

        void InitThemeMode();

        void InitLocaleMode();

        void Init(ITheme theme);

        void InitThemeDirection();
    }
}
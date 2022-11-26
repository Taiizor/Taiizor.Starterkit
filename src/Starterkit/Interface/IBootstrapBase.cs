namespace Starterkit.Interface
{
    public interface IBootstrapBase
    {
        void Init(ITheme theme);

        void InitLocaleMode();

        void InitThemeMode();

        void InitThemeDirection();

        void InitRtl();

        void InitLayout();

        void InitDomain();
    }
}
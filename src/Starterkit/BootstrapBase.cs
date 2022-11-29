using Starterkit.Extension;
using Starterkit.Helper;
using Starterkit.Interface;

namespace Starterkit
{
    public class BootstrapBase : IBootstrapBase
    {
        private ITheme _theme;

        // Init RTL html attributes by checking if RTL is enabled.
        // This function is being called for the html tag
        public void InitRtl()
        {
            if (_theme.IsRtlDirection())
            {
                _theme.AddHtmlAttribute("html", "direction", "rtl");
                _theme.AddHtmlAttribute("html", "dir", "rtl");
                _theme.AddHtmlAttribute("html", "style", "direction: rtl");
            }
        }

        // Init layout
        public void InitLayout()
        {
            _theme.AddHtmlAttribute("body", "id", "kt_app_body");
            _theme.AddHtmlAttribute("body", "data-kt-app-page-loading", "on");
        }

        // Init domain option from settings
        public void InitDomain()
        {
            _theme.SetUri(ThemeSettings.Config.Domain);
            _theme.SetDomain(ThemeSettings.Config.Domain);
        }

        // Init theme mode option from settings
        public void InitThemeMode()
        {
            _theme.SetModeSwitch(ThemeSettings.Config.ModeSwitchEnabled);
            _theme.SetModeDefault(ThemeSettings.Config.ModeDefault);
        }

        // Init locale mode option from settings
        public void InitLocaleMode()
        {
            _theme.SetLocaleSwitch(ThemeSettings.Config.LocaleSwitchEnabled);
            //_theme.SetLocaleDefault(ThemeSettings.Config.LocaleDefault);
        }

        // Global theme initializer
        public void Init(ITheme theme)
        {
            _theme = theme;

            InitLocaleMode();
            InitThemeMode();
            InitThemeDirection();
            InitRtl();
            InitLayout();
            InitDomain();
        }

        // Init theme direction option (RTL or LTR) from settings
        public void InitThemeDirection()
        {
            _theme.SetDirection(Converter.Convert(ThemeSettings.Config.Direction, Enum.DirectionEnum.LTR));
        }
    }
}
using Microsoft.JSInterop;
using Starterkit.Interface;

namespace Starterkit.Extension
{
    class ThemeHelpers : IThemeHelpers
    {
        private readonly IJSRuntime _js;

        public ThemeHelpers(IJSRuntime js)
        {
            _js = js;
        }

        public void AddBodyAttribute(string attribute, string value)
        {
            _ = _js.InvokeVoidAsync("document.body.setAttribute", attribute, value);
        }

        public void RemoveBodyAttribute(string attribute)
        {
            _ = _js.InvokeVoidAsync("document.body.classList.removeAttribute", attribute);
        }

        public void AddBodyClass(string className)
        {
            _ = _js.InvokeVoidAsync("document.body.classList.add", className);
        }

        public void RemoveBodyClass(string className)
        {
            _ = _js.InvokeVoidAsync("document.body.classList.remove", className);
        }
    }
}
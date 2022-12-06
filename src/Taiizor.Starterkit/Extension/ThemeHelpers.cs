using Microsoft.JSInterop;
using Taiizor.Starterkit.Interface;

namespace Taiizor.Starterkit.Extension
{
    public class ThemeHelpers : IThemeHelpers
    {
        private readonly IJSRuntime _js;

        public ThemeHelpers(IJSRuntime js)
        {
            _js = js;
        }

        public async void AddBodyClass(string className)
        {
            await _js.InvokeVoidAsync("document.body.classList.add", className);
        }

        public async void RemoveBodyClass(string className)
        {
            await _js.InvokeVoidAsync("document.body.classList.remove", className);
        }

        public async void RemoveBodyAttribute(string attribute)
        {
            await _js.InvokeVoidAsync("document.body.classList.removeAttribute", attribute);
        }

        public async void AddBodyAttribute(string attribute, string value)
        {
            await _js.InvokeVoidAsync("document.body.setAttribute", attribute, value);
        }
    }
}
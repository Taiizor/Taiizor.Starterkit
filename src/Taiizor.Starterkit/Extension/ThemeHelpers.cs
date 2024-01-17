using Microsoft.JSInterop;
using Taiizor.Starterkit.Interface;

namespace Taiizor.Starterkit.Extension
{
    public class ThemeHelpers(IJSRuntime js) : IThemeHelpers
    {
        public async void AddBodyClass(string className)
        {
            await js.InvokeVoidAsync("document.body.classList.add", className);
        }

        public async void RemoveBodyClass(string className)
        {
            await js.InvokeVoidAsync("document.body.classList.remove", className);
        }

        public async void RemoveBodyAttribute(string attribute)
        {
            await js.InvokeVoidAsync("document.body.classList.removeAttribute", attribute);
        }

        public async void AddBodyAttribute(string attribute, string value)
        {
            await js.InvokeVoidAsync("document.body.setAttribute", attribute, value);
        }
    }
}
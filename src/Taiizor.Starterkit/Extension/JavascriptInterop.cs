using Microsoft.JSInterop;
using Taiizor.Starterkit.Interface;

namespace Taiizor.Starterkit.Extension
{
    public class JavascriptInterop(IJSRuntime js = null, string module = null) : IJavascriptInterop
    {
        private readonly Lazy<Task<IJSObjectReference>> Moduler = new(() => js.InvokeAsync<IJSObjectReference>("import", module).AsTask());

        public async ValueTask<T> InvokeAsync<T>(string identifier, params object[] args)
        {
            IJSObjectReference module = await GetModule();
            return await module.InvokeAsync<T>(identifier, args);
        }

        public async ValueTask InvokeVoidAsync(string identifier, params object[] args)
        {
            IJSObjectReference module = await GetModule();
            await module.InvokeVoidAsync(identifier, args);
        }

        private async ValueTask<IJSObjectReference> GetModule()
        {
            return await Moduler.Value;
        }

        public async ValueTask DisposeAsync()
        {
            if (Moduler.IsValueCreated)
            {
                IJSObjectReference module = await GetModule();
                await module.DisposeAsync();
            }
        }
    }
}
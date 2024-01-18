using Microsoft.JSInterop;
using Taiizor.Starterkit.Interface;

namespace Taiizor.Starterkit.Extension
{
    public class JavascriptInterop : IJavascriptInterop
    {
        private readonly IJSRuntime js;
        private readonly string module;
        private readonly Lazy<Task<IJSObjectReference>> moduler;

        public JavascriptInterop()
        {
            //
        }

        public JavascriptInterop(IJSRuntime js = null)
        {
            this.js = js;
        }

        public JavascriptInterop(IJSRuntime js = null, string module = null)
        {
            this.js = js;
            this.module = module;
            this.moduler = new(() => js.InvokeAsync<IJSObjectReference>("import", module).AsTask());
        }

        public JavascriptInterop(IJSRuntime js = null, string module = null, params string[] identifiers)
        {
            this.js = js;
            this.module = module;
            this.moduler = new(() => js.InvokeAsync<IJSObjectReference>("import", module).AsTask());

            InitializeIdentifiers(identifiers);
        }

        public async ValueTask<T> InvokeAsync<T>(string identifier, params object[] args)
        {
            IJSObjectReference module = await GetModuleAsync();
            return await module.InvokeAsync<T>(identifier, args);
        }

        public async ValueTask InvokeVoidAsync(string identifier, params object[] args)
        {
            IJSObjectReference module = await GetModuleAsync();
            await module.InvokeVoidAsync(identifier, args);
        }

        private async ValueTask<IJSObjectReference> GetModuleAsync()
        {
            return await moduler.Value;
        }

        private void InitializeIdentifiers(string[] identifiers)
        {
            if (identifiers != null && identifiers.Any())
            {
                foreach (string identifier in identifiers)
                {
                    _ = InvokeVoidAsync(identifier);
                }
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (moduler.IsValueCreated)
            {
                IJSObjectReference module = await GetModuleAsync();
                await module.DisposeAsync();
            }
        }
    }
}
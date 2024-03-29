using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using Taiizor.Starterkit.Interface;

namespace Taiizor.Starterkit.Extension
{
    public class JavascriptInterop : IJavascriptInterop
    {
        private readonly IJSRuntime js;
        private readonly string module;
        private readonly Lazy<Task<IJSObjectReference>> moduler;
        private readonly string cache = ThemeSettings.Config.Assets.Version ? $"?v={ThemeSettings.Config.Guid}" : string.Empty;

        public JavascriptInterop(IJSRuntime js = null)
        {
            this.js = js;
        }

        public JavascriptInterop(IJSRuntime js = null, string module = null)
        {
            this.js = js;
            this.module = module;
            this.moduler = new(() => js.InvokeAsync<IJSObjectReference>("import", module + cache).AsTask());
        }

        public JavascriptInterop(IJSRuntime js = null, string module = null, params string[] identifiers)
        {
            this.js = js;
            this.module = module;
            this.moduler = new(() => js.InvokeAsync<IJSObjectReference>("import", module + cache).AsTask());

            _ = InitializeIdentifiersAsync(identifiers);
        }

        public async ValueTask<T> InvokeAsync<T>(string identifier, params object[] args)
        {
            IJSObjectReference module = await GetModuleAsync();
            return await module.InvokeAsync<T>(identifier, TransformLocalizationArgs(args));
        }

        public async ValueTask InvokeVoidAsync(string identifier, params object[] args)
        {
            IJSObjectReference module = await GetModuleAsync();
            await module.InvokeVoidAsync(identifier, TransformLocalizationArgs(args));
        }

        private async ValueTask InitializeIdentifiersAsync(string[] identifiers)
        {
            if (identifiers != null && identifiers.Any())
            {
                foreach (string identifier in identifiers)
                {
                    await InvokeVoidAsync(identifier);
                }
            }
        }

        private async ValueTask<IJSObjectReference> GetModuleAsync()
        {
            return await moduler.Value;
        }

        private object[] TransformLocalizationArgs(object[] args)
        {
            if (args.Any())
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] is LocalizedString localizedString)
                    {
                        if (localizedString.ResourceNotFound)
                        {
                            args[i] = $"[{localizedString.Name}]";
                        }
                        else
                        {
                            args[i] = localizedString.Value;
                        }
                    }
                }
            }

            return args;
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                if (moduler.IsValueCreated)
                {
                    IJSObjectReference module = await GetModuleAsync();

                    if (module != null)
                    {
                        await module.DisposeAsync();
                    }
                }
            }
            catch { }
        }
    }
}
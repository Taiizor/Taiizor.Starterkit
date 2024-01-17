namespace Taiizor.Starterkit.Interface
{
    // Core javascript interop interface
    public interface IJavascriptInterop
    {
        ValueTask InvokeVoidAsync(string identifier, params object[] args);

        ValueTask<T> InvokeAsync<T>(string identifier, params object[] args);
    }
}
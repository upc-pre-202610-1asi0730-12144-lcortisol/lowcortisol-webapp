using Microsoft.JSInterop;

namespace LowCortisol.Presentation.Services;

public class BrowserSessionService
{
    private const string UserIdKey = "app_user_id";
    private readonly IJSRuntime _jsRuntime;

    public BrowserSessionService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task SaveUserIdAsync(Guid userId)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", UserIdKey, userId.ToString());
    }

    public async Task<Guid?> GetUserIdAsync()
    {
        try
        {
            var value = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", UserIdKey);

            if (string.IsNullOrWhiteSpace(value))
                return null;

            return Guid.TryParse(value, out var id) ? id : null;
        }
        catch
        {
            return null;
        }
    }

    public async Task ClearAsync()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", UserIdKey);
    }
}
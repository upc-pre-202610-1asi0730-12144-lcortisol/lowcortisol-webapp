using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace LowCortisol.Presentation.Services;

public class BrowserSessionService
{
    private const string UserIdKey = "lowcortisol.user-id";
    private readonly ProtectedLocalStorage _storage;

    public BrowserSessionService(ProtectedLocalStorage storage)
    {
        _storage = storage;
    }

    public async Task SaveUserIdAsync(Guid userId)
    {
        await _storage.SetAsync(UserIdKey, userId);
    }

    public async Task<Guid?> GetUserIdAsync()
    {
        var result = await _storage.GetAsync<Guid>(UserIdKey);
        return result.Success ? result.Value : null;
    }

    public async Task ClearAsync()
    {
        await _storage.DeleteAsync(UserIdKey);
    }
}
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.JSInterop;

namespace LowCortisol.Presentation.Services;

public class I18nService
{
    private readonly IWebHostEnvironment _environment;
    private readonly IJSRuntime _jsRuntime;
    private Dictionary<string, string> _translations = new();

    public string CurrentLanguage { get; private set; } = "es";

    public event Action? OnChange;

    public I18nService(IWebHostEnvironment environment, IJSRuntime jsRuntime)
    {
        _environment = environment;
        _jsRuntime = jsRuntime;
        LoadLanguage("es");
    }

    public string this[string key]
        => _translations.TryGetValue(key, out var value) ? value : key;

    public async Task InitializeAsync()
    {
        try
        {
            var language = await _jsRuntime.InvokeAsync<string>("appLang.get");

            if (string.IsNullOrWhiteSpace(language))
                language = "es";

            if (language != "es" && language != "en" && language != "pt")
                language = "es";

            LoadLanguage(language);
            OnChange?.Invoke();
        }
        catch
        {
            LoadLanguage("es");
            OnChange?.Invoke();
        }
    }

    public async Task SetLanguageAsync(string language)
    {
        if (string.IsNullOrWhiteSpace(language))
            return;

        if (language != "es" && language != "en" && language != "pt")
            return;

        LoadLanguage(language);

        try
        {
            await _jsRuntime.InvokeVoidAsync("appLang.set", language);
        }
        catch
        {
        }

        OnChange?.Invoke();
    }

    private void LoadLanguage(string language)
    {
        var filePath = Path.Combine(_environment.WebRootPath, "i18n", $"{language}.json");

        if (!File.Exists(filePath))
            return;

        var json = File.ReadAllText(filePath);
        var data = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

        if (data is null)
            return;

        _translations = data;
        CurrentLanguage = language;
    }
}
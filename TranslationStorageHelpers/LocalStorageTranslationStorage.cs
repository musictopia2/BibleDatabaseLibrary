using BasicBlazorLibrary.Helpers;
using Microsoft.JSInterop;

namespace BibleDatabaseLibrary.TranslationStorageHelpers;
public class LocalStorageTranslationStorage(IJSRuntime js) : ITranslationStorage
{
    public string CurrentTranslation { get; private set; } = dd1.DefaultTranslation; //start out with default translation.
    public async Task GetTranslationAsync()
    {
        string text = await js.StorageGetStringAsync("translationUsed");
        if (string.IsNullOrEmpty(text) == false)
        {
            CurrentTranslation = text;
        }
    }
    public async Task SaveTranslationAsync(string translation)
    {
        await js.StorageSetStringAsync("translationUsed", translation);
        CurrentTranslation = translation;
    }
}
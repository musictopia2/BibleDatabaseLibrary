namespace BibleDatabaseLibrary.TranslationStorageHelpers;
public class TranslationFileStorage(ITranslationPath path) : ITranslationStorage
{
    public string CurrentTranslation { get; private set; } = dd1.DefaultTranslation;
    async Task ITranslationStorage.GetTranslationAsync()
    {
        if (ff1.FileExists(path.TranslationPath) == true)
        {
            CurrentTranslation = await ff1.AllTextAsync(path.TranslationPath);
        }
    }
    async Task ITranslationStorage.SaveTranslationAsync(string translation)
    {
        CurrentTranslation = translation; //i think go ahead and save it.
        await ff1.WriteAllTextAsync(path.TranslationPath, CurrentTranslation);
    }
}
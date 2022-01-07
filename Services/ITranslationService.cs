namespace BibleDatabaseLibrary.Services;
public interface ITranslationService
{
    Task<BasicList<TranslationInformation>> ListTranslationsAsync();
    string DefaultTranslationAbb { get; }
}
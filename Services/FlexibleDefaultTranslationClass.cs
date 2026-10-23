namespace BibleDatabaseLibrary.Services;
public class FlexibleDefaultTranslationClass(ITranslationStorage storage) : IFlexibleDefaultTranslationService
{
    string IFlexibleDefaultTranslationService.DefaultTranslationAbb => storage.CurrentTranslation;
}
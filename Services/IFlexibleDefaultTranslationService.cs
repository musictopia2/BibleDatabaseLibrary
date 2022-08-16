namespace BibleDatabaseLibrary.Services;
public interface IFlexibleDefaultTranslationService
{
    string DefaultTranslationAbb { get; } //you have to make sure you choose a valid translation.
    //decided to have in the bible database so somebody can create an implementation without taking a reference to the bible content itself.
}
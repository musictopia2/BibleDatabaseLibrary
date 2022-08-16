namespace BibleDatabaseLibrary.Services;
public class BibleContent : IBibleContent
{
    //decided to put the ibiblecontent in the database so whoever uses it does not even need to know about a 5 day reading program.  does not even care about any of that anyways.
    private readonly IBookDataService _book;
    private readonly ITranslationService _translation;
    public BibleContent(IBookDataService book, ITranslationService translation)
    {
        _book = book;
        _translation = translation;
    }
    Task<BasicList<string>> IBibleContent.GetBookChaptersDataAsync(string bookName, int chapterFrom, int chapterTo)
    {
        using BibleContext bibs = new(_book, _translation);
        return bibs.GetVersesAsync(bookName, chapterFrom, chapterTo);
    }
}
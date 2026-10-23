namespace BibleDatabaseLibrary.Services;
public class BibleContent(IBookDataService book, ITranslationService translation) : IBibleContent
{
    Task<BasicList<string>> IBibleContent.GetBookChaptersDataAsync(string bookName, int chapterFrom, int chapterTo)
    {
        using BibleContext bibs = new(book, translation);
        return bibs.GetVersesAsync(bookName, chapterFrom, chapterTo);
    }
}
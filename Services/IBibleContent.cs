namespace BibleDatabaseLibrary.Services;
public interface IBibleContent
{
    Task<BasicList<string>> GetBookChaptersDataAsync(string bookName, int chapterFrom, int chapterTo);
}
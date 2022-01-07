namespace BibleDatabaseLibrary.Services;
public interface IBookDataService
{
    Task<BookInformation> GetBookInformationAsync(string name, string translationAbb);
}
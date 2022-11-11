namespace BibleDatabaseLibrary.Services;
public class BookListService
{
    //i like the idea of making it static since there is no need to register this time.
    public static async Task<BasicList<string>> GetBookListAsync()
    {
        BasicList<string> output = await rr1.GetResourceAsync<BasicList<string>>();
        output.Sort();
        return output; //i guess no problem in sorting (hopefully no performance penalty).
    }
}
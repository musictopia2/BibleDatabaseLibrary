namespace BibleDatabaseLibrary.MainClasses;
public class BibleContext : IDisposable
{
    private bool _disposedValue;
    private readonly IBookDataService _dataService;
    private readonly ITranslationService _translationService;
    public string TranslationUsed { get; set; } = ""; //if somebody chooses a translation that is not valid, hopefully will raise proper errors.
    protected static string GetText(BasicList<string> thisList)
    {
        var cats = new StrCat();
        thisList.ForEach(thisInfo =>
        {
            thisInfo = thisInfo.Replace(Constants.VBTab, "");
            do
            {
                if (thisInfo.Contains("  ") == false)
                {
                    break;
                }
                thisInfo = thisInfo.Replace("  ", " ");
            } while (true);
            cats.AddToString(thisInfo.Trim(), Constants.VBCrLf);
        });
        return cats.GetInfo();
    }
    public async Task<BasicList<Verse>> GetVersesAsync(string bookName)
    {
        if (bookName == "")
        {
            throw new CustomBasicException("Book Name Can't Be Blank");
        }
        BookInformation bookInfo = await _dataService.GetBookInformationAsync(bookName, TranslationUsed);
        return bookInfo.VerseList;
    }
    public static BasicList<string> GetVerses(BasicList<Verse> thisList)
    {
        BasicList<string> output = new();
        thisList.ForEach(thisItem =>
        {
            string newText;
            string thisInfo = thisItem.Text;
            thisInfo = thisInfo.Replace(Constants.VBTab, " ");
            newText = $"{thisItem.Chapter}:{thisItem.Number}.  {thisInfo}";
            output.Add(newText);
        });
        return output;
    }
    public async Task<int> GetChapterCountAsync(string bookName)
    {
        var firstList = await GetVersesAsync(bookName);
        var nextList = firstList.GroupBy(x => x.Chapter);
        return nextList.Count();
    }
    public async Task<BasicList<string>> GetVersesAsync(string bookName, int chapter)
    {
        var firstList = await GetVersesAsync(bookName);
        firstList.KeepConditionalItems(items => items.Chapter == chapter);
        return GetVerses(firstList);
    }
    public async Task<BasicList<string>> GetVersesAsync(string bookName, int chapterFrom, int chapterTo)
    {
        var firstList = await GetVersesAsync(bookName);
        firstList.KeepConditionalItems(items => items.Chapter >= chapterFrom && items.Chapter <= chapterTo);
        BasicList<string> output = new();
        firstList.ForEach(thisItem =>
        {
            string newText;
            string thisInfo = thisItem.Text;
            thisInfo = thisInfo.Replace(Constants.VBTab, " ");
            newText = $"{bookName} {thisItem.Chapter}:{thisItem.Number}.  {thisInfo}";
            output.Add(newText);
        });
        return output;
    }
    public async Task<BasicList<string>> GetVersesAsync(string bookName, int chapter, int verseFrom, int verseTo)
    {
        var firstList = await GetVersesAsync(bookName);
        firstList.KeepConditionalItems(xxx => xxx.Chapter == chapter);
        firstList.KeepConditionalItems(xxx => xxx.Number >= verseFrom && xxx.Number <= verseTo);
        var output = GetVerses(firstList);
        return output;
    }
    public async Task<BasicList<string>> GetVersesAsync(string bookName, int chapterFrom, int verseFrom, int chapterTo, int verseTo)
    {
        if (chapterFrom == chapterTo)
        {
            return await GetVersesAsync(bookName, chapterFrom, verseFrom, verseTo);
        }
        var firstList = await GetVersesAsync(bookName);
        firstList.KeepConditionalItems(xx => xx.Chapter >= chapterFrom && xx.Chapter <= chapterTo);
        firstList.RemoveAllOnly(xx => xx.Chapter == chapterFrom && xx.Number < verseFrom);
        firstList.RemoveAllOnly(xx => xx.Chapter == chapterTo && xx.Number > verseTo);
        var output = GetVerses(firstList);
        return output;
    }
    public BibleContext(IBookDataService dataService, ITranslationService translationService)
    {
        _dataService = dataService;
        _translationService = translationService;
        TranslationUsed = _translationService.DefaultTranslationAbb;
    }
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {

            }
            _disposedValue = true;
        }
    }
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
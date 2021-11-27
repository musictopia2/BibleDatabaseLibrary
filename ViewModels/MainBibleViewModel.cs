namespace BibleDatabaseLibrary.ViewModels;
public class MainBibleViewModel
{
    private readonly IMessageBox _message;
    public MainBibleViewModel(IMessageBox message)
    {
        _message = message;
    }
    private BasicList<Verse> _verseList = new(); //to avoid the risk of null reference exceptions.
    public TranslationInformation? Translation { get; set; }
    public int? ManuallySelectedVerse { get; set; }
    public string BookName { get; set; } = "";
    public int Chapter { get; set; }
    public static async Task<BasicList<string>> GetBookListAsync()
    {
        using var dats = new BibleContext();
        return await dats.ListBooksAsync(); // i think
    }
    public BasicList<int> GetVerseList()
    {
        if (Chapter == 0)
        {
            throw new CustomBasicException("Must have the chapter entered in order to get the verse list");
        }
        return GetVerseList(Chapter);
    }
    public BasicList<int> GetVerseList(int chapter)
    {
        return _verseList.Where(items => items.Chapter == chapter).Select(items => items.Number).ToBasicList();
    }
    public static async Task<BasicList<TranslationInformation>> GetTranslationListAsync()
    {
        using BibleContext dats = new();
        return await dats.ListTranslationsAsync();
    }
    public BasicList<string> GetText()
    {
        return _verseList.Where(items => items.Chapter == Chapter).Select(items => $"{items.Chapter}: {items.Number}.  {items.Text}").ToBasicList();
    }
    public int GetChapterCount()
    {
        return _verseList.Max(xxx => xxx.Chapter);
    }
    public BasicList<int> GetChapterList()
    {

        return _verseList.GroupBy(items => items.Chapter).Select(items => items.Key).ToBasicList();
    }
    public async Task<HistoricData> GetRandomVerseAsync(BasicList<string> bookList)
    {
        HistoricData thisVerse = new();
        BookInformation thisBook;
        thisVerse.Book = bookList.GetRandomItem();
        using (BibleContext dats = new())
        {
            if (Translation != null)
            {
                dats.TranslationUsed = Translation.TranslationAbb;
            }
            thisBook = await dats.GetBookInfoAsync(thisVerse.Book);
        }
        _verseList = thisBook.VerseList;
        var CList = GetChapterList();
        thisVerse.Chapter = CList.GetRandomItem();
        var tempList = thisBook.ParagraphList.Where(items => items.Chapter == thisVerse.Chapter).ToBasicList();
        if (tempList.Count == 0)
        {
            thisVerse.Verse = 1;
            return thisVerse;
        }
        var thisP = tempList.GetRandomItem();
        thisVerse.Verse = thisP.VerseStartingAt;
        return thisVerse;
    }
    public MobileReadData GetMobileReadData(HistoricData ThisHistory)
    {
        if (Translation == null)
        {
            throw new CustomBasicException("You must choose translation before getting mobile reading data");
        }
        var tempList = _verseList.Where(items => items.Chapter == ThisHistory.Chapter).OrderBy(items => items.Number).ToBasicList();
        var thisItem = tempList.Single(items => items.Number == ThisHistory.Verse);
        MobileReadData thisMobile = new();
        int index;
        BookName = ThisHistory.Book;
        index = tempList.IndexOf(thisItem);
        using (BibleContext bibs = new())
        {
            bibs.TranslationUsed = Translation.TranslationAbb;
            thisMobile.TextList = BibleContext.GetVerses(tempList);
            thisMobile.StartReadingAt = thisMobile.TextList[index];
        }
        return thisMobile;
    }
    public void Clear()
    {
        BookName = "";
        ManuallySelectedVerse = null;
        Chapter = 0;
        BibleDelegates.Cleared.Invoke(); //to stop the overflows.
    }
    public async Task TranslationChosenAsync()
    {
        if (Chapter > 0 && BookName != "")
        {
            using var dats = new BibleContext();
            if (Translation != null)
            {
                dats.TranslationUsed = Translation.TranslationAbb;
            }
            _verseList = await dats.GetVersesAsync(BookName);
        }
        BibleDelegates.ChoseTranslation.Invoke();
    }
    public static async Task<BasicList<string>> GetVersesAsync(string translationabb, string bookName, int chapterFrom, int verseFrom, int chapterTo, int verseTo)
    {
        using var dats = new BibleContext();
        dats.TranslationUsed = translationabb;
        var output = await dats.GetVersesAsync(bookName, chapterFrom, verseFrom, chapterTo, verseTo);
        return output;
    }
    public async Task BookChosenAsync(string book)
    {
        if (!string.IsNullOrWhiteSpace(book))
        {
            BookName = book;
        }
        if (BookName == "")
        {
            await _message.ShowMessageAsync("You must choose book name first");
            return;
        }
        Chapter = 0;
        ManuallySelectedVerse = null;
        using (var dats = new BibleContext())
        {
            if (Translation != null)
            {
                dats.TranslationUsed = Translation.TranslationAbb;
            }
            _verseList = await dats.GetVersesAsync(BookName);
        }
        BibleDelegates.ChoseBook.Invoke();
    }
    public bool DidChooseBook()
    {
        return !string.IsNullOrWhiteSpace(BookName);
    }
    public async Task ChapterChosenAsync()
    {
        if (DidChooseBook() == false)
        {
            return;
        }
        if (Chapter == 0)
        {
            await _message.ShowMessageAsync("You must choose a chapter first");
            return;
        }
        BibleDelegates.ChoseChapters.Invoke();
    }
    public async Task MobileChapterAsync(string chapter)
    {
        Chapter = int.Parse(chapter);
        await ChapterChosenAsync();
    }
    public void VerseChosen(string verse)
    {
        ManuallySelectedVerse = int.Parse(verse);
        BibleDelegates.ChoseVerses.Invoke();
    }
    public async Task SetSavedTranslationAsync(string translationAbb)
    {
        var firstList = await GetTranslationListAsync();
        Translation = firstList.Single(xx => xx.TranslationAbb == translationAbb);
    }
    public void SetDefaultTranslation(BasicList<TranslationInformation> listUsed)
    {
        Translation = listUsed.Single(items => items.TranslationAbb == "ICB");
    }
    public async Task SetPhoneDefaultAsync()
    {
        var firstList = await GetTranslationListAsync();
        Translation = firstList.Single(items => items.TranslationAbb == "NLT");
    }
    public async Task ManuallyChoseTranslationAsync() //to support mobile.
    {
        if (string.IsNullOrEmpty(BookName))
        {
            throw new CustomBasicException("Can't be blank when manually choosing translation");
        }
        if (Translation == null)
        {
            throw new CustomBasicException("You must choose translation in order to manually choose something");
        }
        using BibleContext dats = new();
        dats.TranslationUsed = Translation.TranslationAbb;
        _verseList = await dats.GetVersesAsync(BookName);
    }
}
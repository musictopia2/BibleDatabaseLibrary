namespace BibleDatabaseLibrary.Entities;
public class BookInformation
{
    public BasicList<ParagraphInformation> ParagraphList { get; set; } = new();
    public BasicList<Verse> VerseList { get; set; } = new();
}
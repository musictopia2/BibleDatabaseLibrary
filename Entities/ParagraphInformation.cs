namespace BibleDatabaseLibrary.Entities;
public class ParagraphInformation
{
    public int VerseStartingAt { get; set; }
    public string ParagraphTitle { get; set; } = "";
    public int Chapter { get; set; }
}
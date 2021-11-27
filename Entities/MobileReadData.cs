namespace BibleDatabaseLibrary.Entities;
public class MobileReadData
{
    public string StartReadingAt { get; set; } = "";
    public BasicList<string> TextList = new();
}
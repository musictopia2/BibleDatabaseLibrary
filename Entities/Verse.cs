namespace BibleDatabaseLibrary.Entities;
public class Verse
{
    public int Chapter { get; set; }
    public int Number { get; set; }
    public string Text { get; set; } = "";
}
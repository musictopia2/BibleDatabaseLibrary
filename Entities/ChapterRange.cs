namespace BibleDatabaseLibrary.Entities;
/// <summary>
/// the purpose of this is to be used for a bible reading schedule.  does not matter what is chosen for this single group item.
/// </summary>
public class ChapterRange
{
    public int ChapterFrom { get; set; }
    public int ChapterTo { get; set; }
    public string BookName { get; set; } = "";
    public bool Completed { get; set; } //sometimes i need this and sometimes i don't.  does not hurt to put.  if not needed, then gets ignored.
}
namespace BibleDatabaseLibrary.Entities;
public class HistoricData
{
    public string Book { get; set; } = "";
    public int Chapter { get; set; }
    public int Verse { get; set; }
    public string Title
    {
        get
        {
            return ToString();
        }
    }
    public override string ToString()
    {
        return Book + " " + Chapter + ":" + Verse;
    }
}
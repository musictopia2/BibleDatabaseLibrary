namespace BibleDatabaseLibrary.Entities;
public class TranslationInformation : IComparable<TranslationInformation>
{
    public string TranslationName { get; set; } = "";
    public string TranslationAbb { get; set; } = "";
    int IComparable<TranslationInformation>.CompareTo(TranslationInformation? other)
    {
        return TranslationName.CompareTo(other!.TranslationName);
    }
}
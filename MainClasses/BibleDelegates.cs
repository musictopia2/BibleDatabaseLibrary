namespace BibleDatabaseLibrary.MainClasses;
public static class BibleDelegates //i will not use this from serverside.  otherwise, major problem.
{
    public static Action Cleared { get; set; } = () => { };
    public static Action ChoseVerses { get; set; } = () => { };
    public static Action ChoseBook { get; set; } = () => { throw new CustomBasicException("Needs to populate the chose book.   Rethink"); };
    public static Action ChoseChapters { get; set; } = () => { throw new CustomBasicException("Needs to populate chapters.  Rethink"); };
    public static Action ChoseTranslation { get; set; } = () => { };
}
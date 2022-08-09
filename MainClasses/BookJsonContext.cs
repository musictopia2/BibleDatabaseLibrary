namespace BibleDatabaseLibrary.MainClasses;
[JsonSerializable(typeof(BookInformation))] //this can be registered to improve performance.
public partial class BookJsonContext : JsonSerializerContext //trying this so i can use source generators.  hopefully my idea works.
{
}
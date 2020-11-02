using LiteDB;

namespace Focusmark.SDK
{
    public interface IDatabaseFactory
    {
        ILiteDatabase GetDatabase(string databasePath);
    }
}

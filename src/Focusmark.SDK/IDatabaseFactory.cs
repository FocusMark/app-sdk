using LiteDB;

namespace FocusMark.SDK
{
    public interface IDatabaseFactory
    {
        ILiteDatabase GetDatabase(string databasePath);
    }
}

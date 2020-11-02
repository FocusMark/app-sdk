using LiteDB;
using System.IO;

namespace FocusMark.SDK
{
    public class LiteDatabaseFactory : IDatabaseFactory
    {
        public ILiteDatabase GetDatabase(string databaseName)
        {
            string rootDirectory = Directory.GetCurrentDirectory();
            return new LiteDatabase($"{rootDirectory}\\{databaseName}");
        }
    }
}

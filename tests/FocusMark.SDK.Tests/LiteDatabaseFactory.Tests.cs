using LiteDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace FocusMark.SDK
{
    [TestClass]
    public class LiteDatabaseFactoryTests
    {
        [TestCleanup]
        public void CleanupTests()
        {
            string rootDirectory = Directory.GetCurrentDirectory();
            string[] files = Directory.GetFiles(rootDirectory, $"{nameof(LiteDatabaseFactoryTests)}_*.db");
            foreach(string dbFile in files)
            {
                File.Delete(dbFile);
            }
        }

        [TestMethod]
        public void GetDatabase_ReturnsLiteDatabaseInstance()
        {
            // Arrange
            string databaseName = $"{nameof(LiteDatabaseFactoryTests)}_{nameof(GetDatabase_ReturnsLiteDatabaseInstance)}_{DateTimeOffset.Now.ToUnixTimeMilliseconds()}.db";
            IDatabaseFactory databaseFactory = new LiteDatabaseFactory();

            // Act
            ILiteDatabase database = databaseFactory.GetDatabase(databaseName);
            database.Dispose();

            // Assert
            Assert.IsNotNull(database);
        }
    }
}

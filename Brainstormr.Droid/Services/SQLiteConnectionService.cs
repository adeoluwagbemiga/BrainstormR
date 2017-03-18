using System;
using Brainstormr.Portable.LocalDb;
using SQLite.Net.Platform.XamarinAndroid;
using SQLite.Net;
using System.IO;

namespace Brainstormr.Droid.Services
{
    public class SQLiteConnectionService : ISQLiteConnectionService
    {
        public SQLiteConnection getConnection()
        {
            // string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "columbus.db3");
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
                                                                                                                //  string libraryPath = System.IO.Path.Combine(documentsPath, "../Library/");
            Directory.CreateDirectory(documentsPath);
            var dbPath = Path.Combine(documentsPath, "brainstormr.db3");
            bool fileexist = File.Exists(dbPath);
            var db = new SQLiteConnection(new SQLitePlatformAndroid(), dbPath);

            return db;
        }

        public DateTime getTodaysDate()
        {
            return DateTime.Now;
        }
    }
}
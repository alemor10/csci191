using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using SocialApp.iOS;
using SocialApp.Services;
using SocialApp.iOS.Services;

[assembly: Dependency(typeof(SQLiteDb))]
namespace SocialApp.iOS.Services
{
    public class SQLiteDb : ISQLiteDB
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "MySQLite.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}

using System;
using SQLite;


//interface for SQLiteDB
namespace SocialApp.Services
{
    public interface ISQLiteDB
    {
        SQLiteAsyncConnection GetConnection();
    }
}

using System;
using SQLite;

namespace SocialApp.Services
{
    public interface ISQLiteDB
    {
        SQLiteAsyncConnection GetConnection();
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using SocialApp.Models;
using SocialApp.Services;
using SocialApp.ModelViews;
using SQLite;


//mySQL function
namespace SocialApp.Services
{
    public class SQLitePicturePosts : IPicturePostStore
    {
        private SQLiteAsyncConnection _connection;

        public SQLitePicturePosts(ISQLiteDB db)
        {
            _connection = db.GetConnection();
            _connection.CreateTableAsync<PicturePost>();
        }

        public async Task<IEnumerable<PicturePost>> GetPicturePostsAsync()
        {
            return await _connection.Table<PicturePost>().ToListAsync();
        }

        public async Task DeletePicturePost(PicturePost contact)
        {
            await _connection.DeleteAsync(contact);
        }

        public async Task AddPicturePost(PicturePost contact)
        {
            await _connection.InsertAsync(contact);
        }

        public async Task UpdatePicturePost(PicturePost contact)
        {
            await _connection.UpdateAsync(contact);
        }

        public async Task<PicturePost> GetPicturePost(int id)
        {
            return await _connection.FindAsync<PicturePost>(id);
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace SocialApp.Models
{
	public class postDatabase
	{
		readonly SQLiteAsyncConnection database;

    //setting up database
    public postDatabase(string dbPath)
		{
			database = new SQLiteAsyncConnection(dbPath);
			database.CreateTableAsync<PicturePost>().Wait();
		}

    // adds a Picture Post
		public Task <List<PicturePost>> GetPicturePostAsync()
		{
			return database.Table<PicturePost>().ToListAsync();
		}

    // Runs a query, but not yet setup to query our information just copied from the sqlite set up from Xamarin forms website
		/*public Task<List<PicturePost>> GetPicturePostNotDoneAsync()
		{
			return database.QueryAsync<PicturePost>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
		}*/

    //Saving a post to the database
		public Task<int> SavePicturePostAsync(PicturePost post)
		{
				return database.InsertAsync(post);
		}

    //Deleting post from database
		public Task<int> DeletePicturePostAsync(PicturePost post)
		{
			return database.DeleteAsync(post);
		}
	}
}

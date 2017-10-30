using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
namespace REST.Entity
{
    public class Database
    {
        // SQLite connection
        private SQLiteAsyncConnection database;

        public Database(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Photo>().Wait();
        }

        // Query
        public Task<List<Photo>> GetItemsAsync()
        {
            return database.Table<Photo>().ToListAsync();
        }

        // Query using SQL query string
        public Task<List<Photo>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Photo>("SELECT * FROM [Photo]");
        }

        public Task<List<Photo>> GetItemsNotDoneAsync2(int id)
        {
            return database.QueryAsync<Photo>("SELECT * FROM [Photo] WHERE albumId=" + id);
        }

        // Query using LINQ
        public Task<Photo> GetItemAsync(int id)
        {
            return database.Table<Photo>().Where(i => i.id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Photo item)
        {
            if (item.id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }
        public Task<int> SaveItemsAsync(List<Photo> item)
        {
            return database.InsertAllAsync(item);
        }

        public Task<int> DeleteItemAsync(Photo item)
        {
            return database.DeleteAsync(item);
        }

        public void ResetTable()
        {
            database.DropTableAsync<Photo>().Wait();
            database.CreateTableAsync<Photo>().Wait();

        }
    }
}
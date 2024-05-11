using WeightBalance.Models;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightBalance.Data
{
    //public class AircraftDatabase
    //{
        //SQLiteAsyncConnection Database;

        //public AircraftDatabase()
        //{
        //}

        //async Task Init()
        //{
        //    if (Database is not null)
        //        return;

        //    Database = new SQLiteAsyncConnection(DbConstants.DatabasePath, DbConstants.Flags);
        //    var result = await Database.CreateTableAsync<Aircraft>();
        //}

        //public async Task<List<Aircraft>> GetItemsAsync()
        //{
        //    await Init();
        //    return await Database.Table<Aircraft>().ToListAsync();
        //}

        //public async Task<List<Aircraft>> GetItemsNotDoneAsync()
        //{
        //    await Init();
        //    return await Database.Table<Aircraft>().Where(t => t.Done).ToListAsync();

        //    // SQL queries are also possible
        //    //return await Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        //}

        //public async Task<Aircraft> GetItemAsync(int id)
        //{
        //    await Init();
        //    return await Database.Table<Aircraft>().Where(i => i.ID == id).FirstOrDefaultAsync();
        //}

        //public async Task<int> SaveItemAsync(Aircraft item)
        //{
        //    await Init();
        //    if (item.ID != 0)
        //    {
        //        return await Database.UpdateAsync(item);
        //    }
        //    else
        //    {
        //        return await Database.InsertAsync(item);
        //    }
        //}

        //public async Task<int> DeleteItemAsync(Aircraft item)
        //{
        //    await Init();
        //    return await Database.DeleteAsync(item);
    //    //}
    //}
}

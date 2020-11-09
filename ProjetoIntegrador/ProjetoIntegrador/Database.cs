using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace ProjetoIntegrador
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Person>().Wait();
        }

        public Task<List<Person>> GetPeopleAsync()
        {
            return _database.Table<Person>().ToListAsync();
        }

        public Task<Person> GetNoteAsync(int id)
        {
            return _database.Table<Person>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SavePersonAsync(Person person)
        {
            return _database.InsertAsync(person);
        }

        public Task<int> DeleteNoteAsync(Person note)
        {
            return _database.DeleteAsync(note);
        }
    }
}

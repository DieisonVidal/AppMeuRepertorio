
using AppMeuRepertorio.Model;

using SQLite;


using System.Collections.Generic;


using System.Threading.Tasks; 

namespace AppMeuRepertorio.Helper
{
   
    public class SQLiteDatabaseHelper
    {
        
        readonly SQLiteAsyncConnection _conn;

 
        public SQLiteDatabaseHelper(string path)
        {
              
            _conn = new SQLiteAsyncConnection(path);

       
            _conn.CreateTableAsync<Musica>().Wait();
        }


        public Task<int> Insert(Musica p)
        {
            return _conn.InsertAsync(p);
        }


        public Task<List<Musica>> Update(Musica m)
        {
            string sql = "UPDATE Musica SET Titulo=?, Tom=?, Letra=? WHERE id= ? ";
            return _conn.QueryAsync<Musica>(sql, m.Titulo, m.Tom, m.Letra, m.Id);
        }


     
        public Task<List<Musica>> GetAll()
        {
            return _conn.Table<Musica>().ToListAsync();
        }


        public Task<int> Delete(int id)
        {
            return _conn.Table<Musica>().DeleteAsync(i => i.Id == id);
        }


        public Task<List<Musica>> Search(string q)
        {
            string sql = "SELECT * FROM Musica WHERE Titulo LIKE '%" + q + "%' ";

            return _conn.QueryAsync<Musica>(sql);
        }
    }
}

using MauiAppMinhasCompras.Models;
using SQLite;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection connection;

        public SQLiteDatabaseHelper(string path) 
        {
            connection = new SQLiteAsyncConnection(path);
            connection.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> Insert(Produto p) 
        { 
            return connection.InsertAsync(p);
        }

        public Task<List<Produto>> Update(Produto p)
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? Where ID=?";

            return connection.QueryAsync<Produto>(
                sql, p.Descricao, p.Quantidade,p.Preco, p.Id
          );
        }

        public Task<int> Delete(int id)
        {
            return connection.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Produto>> GetAll() 
        {
            return connection.Table<Produto>().ToListAsync();
        }
        public Task<List<Produto>> Search(string q) 
        {
            string sql = "SELECT * Produto WHERE descricao LIKE '%" + q + "%'";

            return connection.QueryAsync<Produto>(sql);
        } 
    }
}

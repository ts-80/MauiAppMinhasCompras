using MauiAppMinhasCompras.Models;
using SQLite;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDataBaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDataBaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> Insert(Produto produto)
        {
            return _conn.InsertAsync(produto);
        }

        public Task<List<Produto>> Update(Produto produto) 
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? Where id=?"; //exemplo
            return _conn.QueryAsync<Produto>(
                sql, produto.Descricao, produto.Quantidade, produto.Preco, produto.Id
                );
        }

        public Task<int> Delete(int id) 
        {
            return _conn.Table<Produto>().DeleteAsync(i=> i.Id == id);
        }

        public Task<List<Produto>> GetAll() 
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        public Task<List<Produto>> Search(string query) 
        {
            string sql = $"SELECT * FROM Produto WHERE descricao LIKE '%{query}%'"; //exemplo
            return _conn.QueryAsync<Produto>(sql);
        }
    }
}

using GameStoreMVC.Models;
using GameStoreMVC.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;  // <-- adicione este using

namespace GameStoreMVC.Repositorio
{
    public class GameRepository : IGameRepository
    {
        private readonly string _connectionString;

        public GameRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Conexao")!;
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            var games = new List<Game>();
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();
            var sql = "SELECT * FROM Games ORDER BY CriadoEm DESC";
            using var cmd = new MySqlCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
                games.Add(MapGame(reader));
            return games;
        }

        public async Task<Game?> GetByIdAsync(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();
            var sql = "SELECT * FROM Games WHERE Id = @id";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return MapGame(reader);
            return null;
        }

        public async Task AddAsync(Game game)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();
            var sql = @"INSERT INTO Games (Nome, Descricao, Preco, ImagemUrl, Categoria, EmDestaque, CriadoEm)
                        VALUES (@nome, @descricao, @preco, @imagemUrl, @categoria, @emDestaque, @criadoEm)";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@nome", game.Nome);
            cmd.Parameters.AddWithValue("@descricao", game.Descricao);
            cmd.Parameters.AddWithValue("@preco", game.Preco);
            cmd.Parameters.AddWithValue("@imagemUrl", (object?)game.ImagemUrl ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@categoria", game.Categoria);
            cmd.Parameters.AddWithValue("@emDestaque", game.EmDestaque);
            cmd.Parameters.AddWithValue("@criadoEm", game.CriadoEm);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(Game game)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();
            var sql = @"UPDATE Games SET Nome = @nome, Descricao = @descricao, Preco = @preco,
                        ImagemUrl = @imagemUrl, Categoria = @categoria, EmDestaque = @emDestaque
                        WHERE Id = @id";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", game.Id);
            cmd.Parameters.AddWithValue("@nome", game.Nome);
            cmd.Parameters.AddWithValue("@descricao", game.Descricao);
            cmd.Parameters.AddWithValue("@preco", game.Preco);
            cmd.Parameters.AddWithValue("@imagemUrl", (object?)game.ImagemUrl ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@categoria", game.Categoria);
            cmd.Parameters.AddWithValue("@emDestaque", game.EmDestaque);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();
            var sql = "DELETE FROM Games WHERE Id = @id";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            await cmd.ExecuteNonQueryAsync();
        }

        // CORREÇÃO: DbDataReader no lugar de MySqlDataReader
        private static Game MapGame(DbDataReader reader) => new()
        {
            Id = reader.GetInt32("Id"),
            Nome = reader.GetString("Nome"),
            Descricao = reader.GetString("Descricao"),
            Preco = reader.GetDecimal("Preco"),
            ImagemUrl = reader.IsDBNull(reader.GetOrdinal("ImagemUrl")) ? null : reader.GetString("ImagemUrl"),
            Categoria = reader.GetString("Categoria"),
            EmDestaque = reader.GetBoolean("EmDestaque"),
            CriadoEm = reader.GetDateTime("CriadoEm")
        };
    }
}
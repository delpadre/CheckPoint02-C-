using GameStoreMVC.Models;
using GameStoreMVC.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;

namespace GameStoreMVC.Repositorio
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Conexao")!;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            var sql = "SELECT * FROM Usuarios WHERE Email = @email LIMIT 1";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@email", email);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new User
                {
                    Id = reader.GetInt32("Id"),
                    Nome = reader.GetString("Nome"),
                    Email = reader.GetString("Email"),
                    SenhaHash = reader.GetString("SenhaHash"),
                    IsAdmin = reader.GetBoolean("IsAdmin")
                };
            }

            return null;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            var sql = "SELECT COUNT(1) FROM Usuarios WHERE Email = @email";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@email", email);

            var count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            return count > 0;
        }

        public async Task AddAsync(User user)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            var sql = @"INSERT INTO Usuarios (Nome, Email, SenhaHash, IsAdmin)
                        VALUES (@nome, @email, @senhaHash, @isAdmin)";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@nome", user.Nome);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@senhaHash", user.SenhaHash);
            cmd.Parameters.AddWithValue("@isAdmin", user.IsAdmin);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}
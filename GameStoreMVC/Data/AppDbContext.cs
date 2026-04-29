using MySql.Data.MySqlClient;

namespace GameStoreMVC.Data
{
    public class AppDbContext
    {
        private readonly string _connectionString;

        public AppDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Conexao")!;
        }

        public MySqlConnection CreateConnection()
            => new MySqlConnection(_connectionString);
    }
}
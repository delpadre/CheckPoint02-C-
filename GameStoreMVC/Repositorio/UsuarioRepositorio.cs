using GameStoreMVC.Interfaces;
using GameStoreMVC.Models;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;
using BCrypt.Net; // Importante para a criptografia

namespace GameStoreMVC.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly string _connectionString;

        public UsuarioRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Conexao")!;
        }


        public Usuario ValidarLogin(string email, string senha)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();

                // AJUSTE 1: Remova o "AND Senha = @senha" daqui. 
                // O SQL não consegue comparar Hash sozinho.
                var sql = "SELECT * FROM Usuarios WHERE Email = @email";

                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@email", email);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Pegamos o Hash que está guardado na coluna Senha
                        //Pegamos o Hash que está guardado no banco
                        string senhaDoBanco = reader["Senha"].ToString()!;

                        // O Verify faz a mágica: compara a senha limpa com o Hash
                        // O BCrypt pega a 'senha' que veio do teclado e valida contra o 'senhaDoBanco' (hash)
                        if (BCrypt.Net.BCrypt.Verify(senha, senhaDoBanco))
                        {
                            return new Usuario
                            {
                                Id = (int)reader["Id"],
                                Email = reader["Email"].ToString()!,
                                Cargo = reader["Cargo"].ToString()!
                            };
                        }
                    }
                }
            }
            return null!; // E-mail não encontrado ou senha não confere
        }


    }
}

namespace GameStoreMVC.Models
{
    // Usado pelo Repositório para representar o Banco
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public string? Cargo { get; set; }
    }
    public class LoginViewModel
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}

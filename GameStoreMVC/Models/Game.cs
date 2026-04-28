using System.ComponentModel.DataAnnotations;

namespace GameStoreMVC.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome do jogo é obrigatório")]
        [StringLength(150)]
        [Display(Name = "Nome do Jogo")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Descrição é obrigatória")]
        [Display(Name = "Descrição Curta")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "Preço é obrigatório")]
        [Range(0.01, 99999.99, ErrorMessage = "Preço deve ser maior que zero")]
        [Display(Name = "Preço (R$)")]
        public decimal Preco { get; set; }

        [Display(Name = "URL da Capa")]
        public string? ImagemUrl { get; set; }

        [Display(Name = "Categoria")]
        public string Categoria { get; set; } = "Ação";

        public bool EmDestaque { get; set; } = false;

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    }
}

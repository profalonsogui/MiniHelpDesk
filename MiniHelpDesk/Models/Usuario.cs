using System.ComponentModel.DataAnnotations;

namespace MiniHelpDesk.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public required string Nome { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Senha { get; set; }

        // Usuario, Tecnico ou Admin
        public string Perfil { get; set; } = "Usuario";
    }
}
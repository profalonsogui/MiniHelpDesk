namespace MiniHelpDesk.Models
{
    // Representa um chamado do sistema
    public class Chamado
    {
        public int Id { get; set; }
        public required string Titulo { get; set; }
        public required string Descricao { get; set; }
    }
}
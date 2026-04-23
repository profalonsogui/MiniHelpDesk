using Microsoft.EntityFrameworkCore;
using MiniHelpDesk.Models;

namespace MiniHelpDesk.Data
{
    public class AppDbContext : DbContext
    {
        // Construtor que recebe as opções de configuração do DbContext
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Representa a tabela Chamados no banco
        public DbSet<Chamado> Chamados { get; set; }
    }
}

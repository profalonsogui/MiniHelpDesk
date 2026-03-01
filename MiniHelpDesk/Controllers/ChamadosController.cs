using Microsoft.AspNetCore.Mvc;
using MiniHelpDesk.Models;

namespace MiniHelpDesk.Controllers
{
    // Controller responsável pelo módulo de Chamados
    // Ele recebe requisições relacionadas a /Chamados
    public class ChamadosController : Controller
    {
        // Action padrão do módulo
        // Quando acessamos /Chamados, essa função é executada
        public IActionResult Index()
        {
            // Lista simulado dadisos de chamados (em um cenário real, isso viria de um banco de dados)
            var chamados = new List<Chamado>
            {
                  new Chamado { Id = 1, Titulo = "Problema de Login", Descricao = "Usuário não consegue fazer login." },
                  new Chamado { Id = 2, Titulo = "Erro na Impressora", Descricao = "A impressora não está funcionando." },
            };
            // Retorna a View correspondente
            // O ASP.NET procura automaticamente por:
            // Views/Chamados/Index.cshtml
            return View(chamados);
        }

        // Action adicional apenas para exemplo didático
        // Será acessada por /Chamados/Sobre
        public IActionResult Sobre()
        {
            // Também retorna uma View chamada Sobre.cshtml
            return View();
        }
    }
}
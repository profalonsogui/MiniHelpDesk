using Microsoft.AspNetCore.Mvc;

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
            // Retorna a View correspondente
            // O ASP.NET procura automaticamente por:
            // Views/Chamados/Index.cshtml
            return View();
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
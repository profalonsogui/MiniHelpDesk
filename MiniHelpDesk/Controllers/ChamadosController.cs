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

        // Action para exibir os detalhes de UM único chamado
        // O parâmetro 'id' é preenchido automaticamente pelo ASP.NET através da URL
        // Exemplo: /Chamados/Detalhes/1 -> o parâmetro 'id' valerá 1
        public IActionResult Detalhes(int id)
        {
            // Por enquanto, como não temos Banco de Dados, vamos "fingir" uma busca.
            // Criamos um objeto novo usando o ID que veio da URL.
            var chamadoRecuperado = new Chamado
            {
                Id = id,
                Titulo = "Chamado Selecionado #" + id,
                Descricao = "Esta é uma descrição detalhada simulada para o chamado de ID " + id
            };

            // Validação simples: Se o ID for zero ou negativo, simulamos um erro de "Não Encontrado"
            if (id <= 0)
            {
                return NotFound(); // Retorna o erro HTTP 404 (Página não encontrada)
            }

            // Enviamos APENAS ESTE objeto para a View (não é mais uma lista!)
            return View(chamadoRecuperado);
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
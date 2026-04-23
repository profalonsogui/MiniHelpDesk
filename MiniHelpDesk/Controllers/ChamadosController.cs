using Microsoft.AspNetCore.Mvc;
using MiniHelpDesk.Models;

namespace MiniHelpDesk.Controllers
{
    // Controller responsável pelo módulo de Chamados
    public class ChamadosController : Controller
    {
        // 🔵 Lista em memória (simula banco de dados)
        private static List<Chamado> chamados = new List<Chamado>
        {
            new Chamado { Id = 1, Titulo = "Problema de Login", Descricao = "Usuário não consegue fazer login." },
            new Chamado { Id = 2, Titulo = "Erro na Impressora", Descricao = "A impressora não está funcionando." }
        };

        // 🔵 Controle de ID automático
        private static int contadorId = 3;

        // LISTAGEM
        public IActionResult Index()
        {
            // Agora usamos a lista em memória (não recria mais toda vez)
            return View(chamados);
        }

        // 🔎 DETALHES (agora REAL, não mais simulado)
        public IActionResult Detalhes(int id)
        {
            // Busca o chamado na lista
            var chamadoRecuperado = chamados.FirstOrDefault(c => c.Id == id);

            // Se não encontrar → 404
            if (chamadoRecuperado == null)
            {
                return NotFound();
            }

            return View(chamadoRecuperado);
        }

        // 📝 FORMULÁRIO (GET)
        public IActionResult Criar()
        {
            return View();
        }

        // 📝 FORMULÁRIO (POST - navegador)
        [HttpPost]
        public IActionResult Criar(Chamado chamado)
        {
            chamado.Id = contadorId++;
            chamados.Add(chamado);

            return RedirectToAction("Index");
        }

        // 🔌 API (POST - Postman)
        [HttpPost]
        [Route("api/chamados")]
        public IActionResult CriarViaApi([FromBody] Chamado chamado)
        {
            chamado.Id = contadorId++;
            chamados.Add(chamado);

            return Ok(chamado);
        }

        // Página extra
        public IActionResult Sobre()
        {
            return View();
        }
    }
}

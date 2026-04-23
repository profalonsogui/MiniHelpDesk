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
            new Chamado 
            { 
                Id = 1, 
                Titulo = "Problema de Login", 
                Descricao = "Usuário não consegue fazer login.",
                Status = "Aberto",
                DataAbertura = DateTime.Now,
                DataFechamento = null
            },
            new Chamado 
            { 
                Id = 2, 
                Titulo = "Erro na Impressora", 
                Descricao = "A impressora não está funcionando.",
                Status = "Fechado",
                DataAbertura = DateTime.Now,
                DataFechamento = null
            }
        };

        // 🔵 Controle de ID automático
        private static int contadorId = 3;

        // 📋 LISTAGEM DE CHAMADOS
        public IActionResult Index()
        {
            // Retorna a lista completa para a View
            return View(chamados);
        }

        // 🔎 DETALHES DE UM CHAMADO
        public IActionResult Detalhes(int id)
        {
            // Busca o chamado pelo ID
            var chamadoRecuperado = chamados.FirstOrDefault(c => c.Id == id); // LINQ para encontrar o chamado com o ID correspondente

            // Se não encontrar → retorna erro 404
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
            // Define ID automático
            chamado.Id = contadorId++;

            // Define valores padrão
            chamado.Status = "Aberto";
            chamado.DataAbertura = DateTime.Now;
            chamado.DataFechamento = null;

            // Adiciona na lista
            chamados.Add(chamado);

            // Redireciona para listagem
            return RedirectToAction("Index");
        }

        // 🔌 API (POST - Postman)
        [HttpPost]
        [Route("api/chamados")]
        public IActionResult CriarViaApi([FromBody] Chamado chamado)
        {
            chamado.Id = contadorId++;

            chamado.Status = "Aberto";
            chamado.DataAbertura = DateTime.Now;
            chamado.DataFechamento = null;

            chamados.Add(chamado);

            return Ok(chamado);
        }

        // ℹ️ Página informativa
        public IActionResult Sobre()
        {
            return View();
        }
    }
}

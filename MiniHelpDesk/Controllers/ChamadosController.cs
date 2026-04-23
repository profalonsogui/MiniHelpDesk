using Microsoft.AspNetCore.Mvc;
using MiniHelpDesk.Models;
using MiniHelpDesk.Data;

namespace MiniHelpDesk.Controllers
{
    public class ChamadosController : Controller
    {
        // 🔵 Campo para acessar o banco de dados
        private readonly AppDbContext _context;

        // 🔵 Injeção do DbContext
        public ChamadosController(AppDbContext context)
        {
            _context = context;
        }

        // 📋 LISTAGEM
        public IActionResult Index()
        {
            // 🔵 Busca todos os chamados no banco e converte para lista
            var chamados = _context.Chamados.ToList();
            return View(chamados);
        }

        // 🔎 DETALHES
        public IActionResult Detalhes(int id)
        {
            // 🔵 Busca o chamado pelo ID
            var chamado = _context.Chamados.FirstOrDefault(c => c.Id == id);

            if (chamado == null)
                return NotFound();

            return View(chamado);
        }

        // 📝 FORMULÁRIO
        public IActionResult Criar()
        {
            return View();
        }

        // 📝 SALVAR (POST)
        [HttpPost]
        public IActionResult Criar(Chamado chamado)
        {
            chamado.Status = "Aberto";
            chamado.DataAbertura = DateTime.Now;
            chamado.DataFechamento = null;

            _context.Chamados.Add(chamado);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // 🔌 API
        [HttpPost]
        [Route("api/chamados")]
        public IActionResult CriarViaApi([FromBody] Chamado chamado)
        {
            chamado.Status = "Aberto";
            chamado.DataAbertura = DateTime.Now;
            chamado.DataFechamento = null;

            _context.Chamados.Add(chamado);
            _context.SaveChanges();

            return Ok(chamado);
        }

        public IActionResult Sobre()
        {
            return View();
        }
    }
}

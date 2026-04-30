using Microsoft.AspNetCore.Mvc;
using MiniHelpDesk.Data;
using MiniHelpDesk.Models;

namespace MiniHelpDesk.Controllers
{
    public class AuthController : Controller
    {
        // Injeção de dependência do contexto do banco de dados
        private readonly AppDbContext _context;

        // Construtor para receber o contexto do banco de dados
        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // Exibe a página de cadastro
        public IActionResult Cadastrar()
        {
            return View();
        }

        // Processa o formulário de cadastro
        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            usuario.Perfil = "Usuario";

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
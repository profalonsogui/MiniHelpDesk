using Microsoft.AspNetCore.Mvc;

namespace MiniHelpDesk.Controllers
{
    public class ChamadosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
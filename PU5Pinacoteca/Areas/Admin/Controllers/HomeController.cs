using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PU5Pinacoteca.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrador, Gestor")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        //INYECTAR EL REPOSITORIO



        public IActionResult Index()
        {
            return View();
        }
    }
}

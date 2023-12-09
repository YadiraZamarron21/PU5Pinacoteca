using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PU5Pinacoteca.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrador, Gestor")]
    [Area("Admin")]
    public class HomeController : Controller
    {
      



        public IActionResult Index()
        {
            return View();
        }
    }
}

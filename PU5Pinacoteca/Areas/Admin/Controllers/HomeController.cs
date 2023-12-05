using Microsoft.AspNetCore.Mvc;

namespace PU5Pinacoteca.Areas.Admin.Controllers
{
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

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace PU5Pinacoteca.Controllers
{
    public class HomeController : Controller
    {
        //INYECTAR EL REPOSITORIO


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Cuadros()
        {
            return View();
        }
        public IActionResult Pintores()
        {
            return View();
        }
        public IActionResult VerCuadro()
        {
            return View();
        }
        public IActionResult VerPintor()
        {
            return View();
        }


        //SIN VISTA:
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult Login()
        //{
        //    return View();
        //}
        public IActionResult Denied()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

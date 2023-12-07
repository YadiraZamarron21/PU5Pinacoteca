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
        public IActionResult VerCuadrosPorPintor()
        {
            return View();
        }


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
        
        
        //SIN VISTA:

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

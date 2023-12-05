using Microsoft.AspNetCore.Mvc;

namespace PU5Pinacoteca.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CuadrosController : Controller
    {
        //INYECTAR EL REPOSITORIO


        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Agregar()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult Agregar()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult Editar(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Editar()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Eliminar()
        {
            return View();
        }
    }
}

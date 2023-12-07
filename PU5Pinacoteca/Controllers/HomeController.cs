using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PU5Pinacoteca.Models.Entities;
using PU5Pinacoteca.Models.ViewModels;
using PU5Pinacoteca.Repositories;

namespace PU5Pinacoteca.Controllers
{
    public class HomeController : Controller
    {
        //INYECTAR EL REPOSITORIO
        private readonly CuadrosRepository cuadrosRepository;
        private readonly Repository<Coleccion> coleccionRepository;
        private readonly Repository<Pintor> pintorRepository;

        public HomeController(CuadrosRepository cuadrosRepository, Repository<Coleccion> coleccionRepository, Repository<Pintor> pintorRepository)
        {
            this.cuadrosRepository = cuadrosRepository;
            this.coleccionRepository = coleccionRepository;
            this.pintorRepository = pintorRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Cuadros()
        {
            CuadrosViewModel vm = new()
            {
                Colecciones = cuadrosRepository.GetAll().GroupBy(x => x.IdColeccionNavigation).Select(x => new ColeccionModel
                {
                    Clasificacion = x.Key.Nombre,
                    Cuadros = cuadrosRepository.GetAll().Where(a => a.IdColeccion == x.Key.Id).Select(x => new CuadroModel
                    {
                        Id = x.Id,
                        Nombre = x.TituloCuadro,
                        NombrePintor = x.IdPintorNavigation.Nombre
                    })
                })
            };
            return View(vm);
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

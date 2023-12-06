using Microsoft.AspNetCore.Mvc;
using PU5Pinacoteca.Areas.Admin.Models;
using PU5Pinacoteca.Models.Entities;
using PU5Pinacoteca.Repositories;

namespace PU5Pinacoteca.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CuadrosController : Controller
    {
        //INYECTAR EL REPOSITORIO
        private readonly CuadrosRepository cuadrosRepos;
        private readonly Repository<Coleccion> coleccionRepos;
        private readonly Repository<Pintor> pintorRepo;
        public CuadrosController(CuadrosRepository cuadrosRepository, Repository<Coleccion> coleccionRepository,Repository<Pintor> pintorRepository)
        {
            cuadrosRepos = cuadrosRepository;
            coleccionRepos = coleccionRepository;
            pintorRepo = pintorRepository;
        }

        public IActionResult Index()
        {
            AdminVerCuadrosViewModel vm = new()
            {
                Colecciones = cuadrosRepos.GetAll().GroupBy(x=>x.IdColeccionNavigation).Select(x=> new AdminVerColeccionModel
                {
                    Clasificacion = x.Key.Nombre,
                    Cuadros = cuadrosRepos.GetAll().Where(a=>a.IdColeccion == x.Key.Id).Select(x=> new AdminVerCuadroModel
                    {
                        Id = x.Id,
                        Nombre = x.TituloCuadro,
                        NombrePintor = x.IdPintorNavigation.Nombre
                    })
                })
            };
            return View(vm);
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

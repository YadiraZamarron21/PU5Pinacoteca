using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PU5Pinacoteca.Helpers;
using PU5Pinacoteca.Models.Entities;
using PU5Pinacoteca.Models.ViewModels;
using PU5Pinacoteca.Repositories;
using System.Security.Claims;

namespace PU5Pinacoteca.Controllers
{
    public class HomeController : Controller
    {
        //INYECTAR EL REPOSITORIO
        private readonly CuadrosRepository cuadrosRepository;
        private readonly Repository<Coleccion> coleccionRepository;
        private readonly PintoresRepository pintorRepository;
        private readonly Repository<Usuarios> usuariosRepository;

        public HomeController(CuadrosRepository cuadrosRepository, Repository<Coleccion> coleccionRepository,PintoresRepository pintorRepository, Repository<Usuarios> usuariosRepository)
        {
            this.cuadrosRepository = cuadrosRepository;
            this.coleccionRepository = coleccionRepository;
            this.pintorRepository = pintorRepository;
            this.usuariosRepository = usuariosRepository;
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
            PintoresViewModel vm = new()
            {
                Paises = pintorRepository.GetAll().GroupBy(x => x.Pais).Select(x => new PaisModel
                {
                    NombrePais = x.Key,
                    Pintores = pintorRepository.GetAll().Where(b =>b.Pais==x.Key).Select(x=> new PintorModel
                    {
                        Id = x.IdPintor,
                        FechaFallec = x.FechaFallecimiento??"Sin registro",
                        FechaNacim = x.FechaNacimiento ?? "Sin registro",
                        Nombre = x.Nombre
                    })
                })
            };
            return View(vm);
        }
        public IActionResult VerCuadro(string id)
        {
            id = id.Replace("-", " ");
            var cuadro = cuadrosRepository.GetByNombre(id);

            if (cuadro == null)
            {
                return RedirectToAction("Index");
            }

            VerCuadroViewModel vm = new()
            {
                Id = cuadro.Id,
                Titulo = cuadro.TituloCuadro,
                Descripcion = cuadro.Descripcion,
                Pintor = cuadro.IdPintorNavigation.Nombre,
                Coleccion = cuadro.IdColeccionNavigation.Nombre,
                FechaPintado = cuadro.FechaPintado,
                Tecnica = cuadro.Tecnica,
                Dimensiones = cuadro.Dimensiones ?? ""
            };
            return View(vm);
        }
        public IActionResult VerPintor(string id)
        {
            id = id.Replace("-", " ");
            var pintor = pintorRepository.GetByNombre(id);

            if (pintor == null)
            {
                return RedirectToAction("Index");
            }

            VerPintorViewModel vm = new()
            {
                Id = pintor.IdPintor,
                Nombre = pintor.Nombre,
                Biografia = pintor.Biografia,
                FechaFallecimiento = pintor.FechaFallecimiento ?? "",
                FechaNacimiento = pintor.FechaNacimiento ?? "",
                Ciudad = pintor.Ciudad,
                Pais = pintor.Pais
            };
            return View(vm);
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


        [HttpPost]
        public IActionResult Login(LoginViewModel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.Correo))
            {
                ModelState.AddModelError("", "Escriba el correo electronico del usuario");
            }
            if (string.IsNullOrWhiteSpace(vm.Contrasena))
            {
                ModelState.AddModelError("", "Escriba la contrasena del usuario");
            }

            if (ModelState.IsValid)
            {
                //ENCUENTRA AL USUARIO CON LAS CREDENCIALES INTRODUCIDAS
                var user = usuariosRepository.GetAll().FirstOrDefault(x => x.Correo == vm.Correo && x.Contrasena == Encriptacion.StringToSHA512(vm.Contrasena));

                if (user == null)
                {
                    ModelState.AddModelError("", "Contrasena o correo electronico incorrectos.");
                }
                else
                {
                   //GENERA LAS CLAIMS
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim("Id", user.Id.ToString()));  
                    claims.Add(new Claim(ClaimTypes.Name, user.Nombre));
                    claims.Add(new Claim(ClaimTypes.Role, user.Rol == 1 ? "Administrador" : "Gestor"));

                    ClaimsIdentity identity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme); //CREACION DE LA CREDENCIAL
                    HttpContext.SignInAsync(new ClaimsPrincipal(identity), new AuthenticationProperties
                    {
                        IsPersistent = false,  //LOGIN PERSISTENTE PARA PRUEBAS
                    });

                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
            }
            return View(vm);
        }


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

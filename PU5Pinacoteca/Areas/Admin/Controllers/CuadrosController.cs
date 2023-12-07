
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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
        public CuadrosController(CuadrosRepository cuadrosRepository, Repository<Coleccion> coleccionRepository, Repository<Pintor> pintorRepository)
        {
            cuadrosRepos = cuadrosRepository;
            coleccionRepos = coleccionRepository;
            pintorRepo = pintorRepository;
        }

        public IActionResult Index()
        {
            AdminVerCuadrosViewModel vm = new()
            {
                Colecciones = cuadrosRepos.GetAll().GroupBy(x => x.IdColeccionNavigation).Select(x => new AdminVerColeccionModel
                {
                    Clasificacion = x.Key.Nombre,
                    Cuadros = cuadrosRepos.GetAll().Where(a => a.IdColeccion == x.Key.Id).Select(x => new AdminVerCuadroModel
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
            AdminAgregarCuadroViewModel vm = new();
            vm.Colecciones = coleccionRepos.GetAll().Select(x => new AdminColeccionModel
            {
                Id = x.Id,
                Nombre = x.Nombre
            });
            vm.Pintores = pintorRepo.GetAll().Select(x => new AdminPintorModel
            {
                Id = x.IdPintor,
                Nombre = x.Nombre
            });
            return View(vm);
        }
        [HttpPost]
        public IActionResult Agregar(AdminAgregarCuadroViewModel vm)
        {
            if (vm.Archivo != null)
            {
                if (vm.Archivo.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("", "Sólo se permiten imagenes JPG");
                }
                if (vm.Archivo.Length > 5000 * 1024)
                {
                    ModelState.AddModelError("", "Sólo se permiten archivos no mayores a 5mb");
                }
            }
            if (string.IsNullOrWhiteSpace(vm.Nombre))
            {
                ModelState.AddModelError("", "Escribe el nombre del cuadro");
            }
            if (string.IsNullOrWhiteSpace(vm.AñoPintado.ToString()))
            {
                ModelState.AddModelError("", "El año no puede estar vacío");
            }
            else
            {
                if (vm.AñoPintado > DateTime.Now.Year)
                {
                    ModelState.AddModelError("", "La fecha no puede ser en un futuro");
                }
            }
            if (string.IsNullOrWhiteSpace(vm.Tecnica))
            {
                ModelState.AddModelError("", "Escribe el nombre de la técnica que se usó");
            }
            if (string.IsNullOrWhiteSpace(vm.Dimensiones))
            {
                ModelState.AddModelError("", "Escriba las dimensiones del cuadro");
            }
            if (string.IsNullOrWhiteSpace(vm.Descripcion))
            {
                ModelState.AddModelError("", "Escriba la descripción del cuadro");
            }
            if (ModelState.IsValid)
            {
                var cuadro = new Cuadro()
                {
                    Descripcion = vm.Descripcion,
                    Dimensiones = vm.Dimensiones,
                    FechaPintado = vm.AñoPintado,
                    Id = vm.Id,
                    IdColeccion = vm.IdColeccion,
                    IdPintor = vm.IdPintor,
                    Tecnica = vm.Tecnica,
                    TituloCuadro = vm.Nombre

                };
                cuadrosRepos.Insert(cuadro);
                if (vm.Archivo == null)
                {
                    System.IO.File.Copy("wwwroot/Cuadros/no-disponible.png", $"wwwroot/Cuadros/{cuadro.Id}.jpg");
                }
                else
                {
                    System.IO.FileStream fs = System.IO.File.Create($"wwwroot/Cuadros/{cuadro.Id}.jpg");
                    vm.Archivo.CopyTo(fs);
                    fs.Close();
                }

                return RedirectToAction("Index");
            }
            vm.Colecciones = coleccionRepos.GetAll().Select(x => new AdminColeccionModel
            {
                Id = x.Id,
                Nombre = x.Nombre
            });
            vm.Pintores = pintorRepo.GetAll().Select(x => new AdminPintorModel
            {
                Id = x.IdPintor,
                Nombre = x.Nombre
            });
            return View(vm);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var cuadro = cuadrosRepos.Get(id);
            if (cuadro == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                AdminAgregarCuadroViewModel vm = new()
                {
                    AñoPintado = cuadro.FechaPintado,
                    IdColeccion = cuadro.IdColeccion,
                    Descripcion = cuadro.Descripcion,
                    Dimensiones = cuadro.Dimensiones,
                    Id = cuadro.Id,
                    IdPintor = cuadro.IdPintor,
                    Nombre = cuadro.TituloCuadro,
                    Tecnica = cuadro.Tecnica

                };
                vm.Colecciones = coleccionRepos.GetAll().Select(x => new AdminColeccionModel
                {
                    Id = x.Id,
                    Nombre = x.Nombre
                });
                vm.Pintores = pintorRepo.GetAll().Select(x => new AdminPintorModel
                {
                    Id = x.IdPintor,
                    Nombre = x.Nombre
                });
                return View(vm);
            }
        }
        [HttpPost]
        public IActionResult Editar(AdminAgregarCuadroViewModel vm)
        {
            if (vm.Archivo != null)
            {
                if (vm.Archivo.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("", "Sólo se permiten imagenes JPG");
                }
                if (vm.Archivo.Length > 5000 * 1024)
                {
                    ModelState.AddModelError("", "Sólo se permiten archivos no mayores a 5mb");
                }
            }
            if (string.IsNullOrWhiteSpace(vm.Nombre))
            {
                ModelState.AddModelError("", "Escribe el nombre del cuadro");
            }
            if (string.IsNullOrWhiteSpace(vm.AñoPintado.ToString()))
            {
                ModelState.AddModelError("", "El año no puede estar vacío");
            }
            else
            {
                if (vm.AñoPintado > DateTime.Now.Year)
                {
                    ModelState.AddModelError("", "La fecha no puede ser en un futuro");
                }
            }
            if (string.IsNullOrWhiteSpace(vm.Tecnica))
            {
                ModelState.AddModelError("", "Escribe el nombre de la técnica que se usó");
            }
            if (string.IsNullOrWhiteSpace(vm.Dimensiones))
            {
                ModelState.AddModelError("", "Escriba las dimensiones del cuadro");
            }
            if (string.IsNullOrWhiteSpace(vm.Descripcion))
            {
                ModelState.AddModelError("", "Escriba la descripción del cuadro");
            }
            if (ModelState.IsValid)
            {
                var cuadro = new Cuadro()
                {
                    Descripcion = vm.Descripcion,
                    Dimensiones = vm.Dimensiones,
                    FechaPintado = vm.AñoPintado,
                    Id = vm.Id,
                    IdColeccion = vm.IdColeccion,
                    IdPintor = vm.IdPintor,
                    Tecnica = vm.Tecnica,
                    TituloCuadro = vm.Nombre

                };
                cuadrosRepos.Update(cuadro);
                if (vm.Archivo != null)
                {
                    System.IO.FileStream fs = System.IO.File.Create($"wwwroot/Cuadros/{cuadro.Id}.jpg");
                    vm.Archivo.CopyTo(fs);
                    fs.Close();
                }

                return RedirectToAction("Index");
            }
            vm.Colecciones = coleccionRepos.GetAll().Select(x => new AdminColeccionModel
            {
                Id = x.Id,
                Nombre = x.Nombre
            });
            vm.Pintores = pintorRepo.GetAll().Select(x => new AdminPintorModel
            {
                Id = x.IdPintor,
                Nombre = x.Nombre
            });
            return View(vm);
        }


        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            var cuadro = cuadrosRepos.Get(id);
            if (cuadro == null)
            {
                return RedirectToAction("Index");
            }
            return View(cuadro);
        }
        [HttpPost]
        public IActionResult Eliminar(Cuadro c)
        {
            var cuadro = cuadrosRepos.Get(c.Id);
            if (cuadro == null)
            {
                return RedirectToAction("Index");
            }
            cuadrosRepos.Delete(cuadro);
            var imagen = $"wwwroot/Cuadros/{cuadro.Id}.jpg";
            if (System.IO.File.Exists(imagen))
            {
                System.IO.File.Delete(imagen);
            }

            return RedirectToAction("Index");

        }
    }
}

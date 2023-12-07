﻿using Microsoft.AspNetCore.Mvc;
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
            AdminAgregarCuadroViewModel vm = new();
            vm.Colecciones = coleccionRepos.GetAll().Select(x => new AdminColeccionModel
            {
                Id = x.Id,
                Nombre = x.Nombre
            });
            vm.Pintores = pintorRepo.GetAll().Select(x=> new AdminPintorModel
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
                if (vm.Archivo.ContentType != "image/jpg")
                {
                    ModelState.AddModelError("", "Sólo se permiten imagenes JPG");
                }
                if (vm.Archivo.Length > 500 * 1024)
                {
                    ModelState.AddModelError("", "Sólo se permiten archivos no mayores a 500kb");
                }
            }
            if (string.IsNullOrWhiteSpace(vm.Nombre))
            {
                ModelState.AddModelError("","Escribe el nombre del cuadro");
            }
            if (string.IsNullOrWhiteSpace(vm.AñoPintado.ToString()))
            {
                ModelState.AddModelError("","El año no puede estar vacío");
            }
            else
            {
                if (vm.AñoPintado > DateTime.Now.Year)
                {
                    ModelState.AddModelError("","La fecha no puede ser en un futuro");
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
                    System.IO.File.Copy("wwwroot/images/burger.png", $"wwwroot/Cuadros/{cuadro.Id}.jpg");
                }
                else
                {
                    System.IO.FileStream fs = System.IO.File.Create($"wwwroot/Cuadros/{cuadro.Id}.png");
                    vm.Archivo.CopyTo(fs);
                    fs.Close();
                }

                return RedirectToAction("Index");
            }
            //vm.Colecciones = 
            return View(vm);
        }

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

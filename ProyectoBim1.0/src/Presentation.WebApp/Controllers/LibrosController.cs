using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; // Asegúrate de incluir esto para IConfiguration
using Domain.Entities; // Asegúrate de que este es el correcto
using Infrastructure.Data; // Asegúrate de que este es el correcto

namespace Presentation.WebApp.Controllers
{
    public class LibrosController : Controller
    {
        private readonly LibrosDbContext _librosDbContext; // Cambiado a LibrosDbContext

        public LibrosController(IConfiguration configuration)
        {
            _librosDbContext = new LibrosDbContext(configuration.GetConnectionString("DefaultConnection"));
        }

        public IActionResult Index()
        {
            var data = _librosDbContext.List(); // Cambiado a _librosDbContext
            return View(data);
        }

        public IActionResult Details(Guid id)
        {
            var data = _librosDbContext.Details(id); // Cambiado a _librosDbContext
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IM253E01Libro data) // Cambiado a IM253E01Libro
        {
            data.Id = Guid.NewGuid();
            _librosDbContext.Create(data); // Cambiado a _librosDbContext
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid id)
        {
            var data = _librosDbContext.Details(id); // Cambiado a _librosDbContext
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(IM253E01Libro data) // Cambiado a IM253E01Libro
        {
            _librosDbContext.Update(data); // Cambiado a Update
            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid id)
        {
            _librosDbContext.Delete(id); // Cambiado a _librosDbContext
            return RedirectToAction("Index");
        }
    }
}

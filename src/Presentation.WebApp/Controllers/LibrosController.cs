using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities; // Asegúrate de que este es el correcto
using Infrastructure.Data; // Asegúrate de que este es el correcto

namespace Presentation.WebApp.Controllers
{
    public class LibrosController : Controller
    {
        private readonly UsuariosDbContext _usuariosDbContext;

        public LibrosController(IConfiguration configuration)
        {
            _usuariosDbContext = new UsuariosDbContext(configuration.GetConnectionString("DefaultConnection"));
        }

        public IActionResult Index()
        {
            var data = _usuariosDbContext.List();
            return View(data);
        }

        public IActionResult Details(Guid id)
        {
            var data = _usuariosDbContext.Details(id);
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IM253E01Usuario data)
        {
            data.Id = Guid.NewGuid();
            _usuariosDbContext.Create(data);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid id)
        {
            var data = _usuariosDbContext.Details(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(IM253E01Usuario data)
        {
            _usuariosDbContext.Edit(data);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid id)
        {
            _usuariosDbContext.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; // Asegúrate de incluir esto para IConfiguration
using Domain.Entities; // Asegúrate de que este es el correcto
using Infrastructure.Data; // Asegúrate de que este es el correcto
using Application.Services; // Reemplaza con el espacio de nombres correcto para FileConverterService

namespace Presentation.WebApp.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UsuariosDbContext _usuariosDbContext;

        public UsuariosController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "El connectionString no puede ser nulo o vacío.");
            }

            _usuariosDbContext = new UsuariosDbContext(connectionString);
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
        public IActionResult Create(IM253E01Usuario data, IFormFile file)
        {
            if (file != null)
            {
                data.Foto = FileConverterService.ConvertToBase64(file.OpenReadStream());
            }

            _usuariosDbContext.Create(data);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid id)
        {
            var data = _usuariosDbContext.Details(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(IM253E01Usuario data, IFormFile file)
        {
            if (file != null)
            {
                data.Foto = FileConverterService.ConvertToBase64(file.OpenReadStream());
            }

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

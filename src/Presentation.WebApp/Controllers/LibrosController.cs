using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using Domain;
using Infrastructure;

namespace Presentation.WebApp.Controllers;

public class LibrosController : Controller
{
      private readonly UsuariosDbContext _UsuariosDbContext;
      public LibrosController(IConfiguration configuration)
      {
            _UsuariosDbContext = new UsuariosDbContext(configuration.GetConnectionString("DefaultConnection"));
      }

      public IActionResult Index()
      {
            var data = _UsuariosDbContext.List();
            return View(data);
      }

      public IActionResult Details(Guid id)
      {
            var data = _UsuariosDbContext.Details(id);
            return View(data);
      }

      public IActionResult Create()
      {
            return View();
      }
      [HttpPost]
      public IActionResult Create(Empleado data)
      {
            data.Id = Guid.NewGuid();
            _UsuariosDbContext.Create(data);
            return RedirectToAction("Index");
      }

      public IActionResult Edit(Guid id)
      {
            var data = _UsuariosDbContext.Details(id);
            return View(data);
      }
      [HttpPost]
      public IActionResult Edit(Empleado data)
      {
            _UsuariosDbContext.Edit(data);
            return RedirectToAction("Index");
      }

      public IActionResult Delete(Guid id)
      {
            _UsuariosDbContext.Delete(id);
            return RedirectToAction("Index");
      }
}
using Microsoft.AspNetCore.Mvc;

using Domain;
using Application;
using Infrastructure;

namespace Presentation.WebApp.Controllers;
public class UsuariosController : Controller
{
    private readonly UsuariosDbContext _UsuariosDbContext;
    public UsuariosController(IConfiguration configuration)
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
    public IActionResult Create(Empleado data, IFormFile file)
    {
        if (file != null)
        {
            data.Foto = FileConverterService.ConvertToBase64(file.OpenReadStream());
        }

        _UsuariosDbContext.Create(data);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(Guid id)
    {
        var data = _UsuariosDbContext.Details(id);
        return View(data);
    }
    [HttpPost]
    public IActionResult Edit(Empleado data, IFormFile file)
    {
        if (file != null)
        {
            data.Foto = FileConverterService.ConvertToBase64(file.OpenReadStream());
        }

        _UsuariosDbContext.Edit(data);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(Guid id)
    {
        _UsuariosDbContext.Delete(id);
        return RedirectToAction("Index");
    }
}
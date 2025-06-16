using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Domain;
using Application;
using Infrastructure;

namespace Presentation.WebApp.Controllers;
public class PrestamosController : Controller
{
    private readonly PrestamosDbContext _prestamosDbContext;
    private readonly EmpleadosDbContext _empleadosDbContext;
    private readonly ProyectosDbContext _proyectosDbContext;

    public PrestamosController(IConfiguration configuration)
    {
        _PrestamosDbContext = new PrestamosDbContext(configuration.GetConnectionString("DefaultConnection"));
        _empleadosDbContext = new EmpleadosDbContext(configuration.GetConnectionString("DefaultConnection"));
        _proyectosDbContext = new ProyectosDbContext(configuration.GetConnectionString("DefaultConnection"));
    }

    public IActionResult Index()
    {
        var data = _PrestamosDbContext.List();
        return View(data);
    }

    public IActionResult Details(Guid id)
    {
        var data = _PrestamosDbContext.Details(id);
        return View(data);
    }

    public IActionResult Create()
    {
        PopulateSelectLists();
        return View();
    }

    [HttpPost]
    public IActionResult Create(Asignacion data)
    {
        _PrestamosDbContext.Create(data);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(Guid id)
    {
        var data = _PrestamosDbContext.Details(id);
        PopulateSelectLists(data);
        return View(data);
    }

    [HttpPost]
    public IActionResult Edit(Asignacion data)
    {
        _PrestamosDbContext.Edit(data);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(Guid id)
    {
        _PrestamosDbContext.Delete(id);
        return RedirectToAction("Index");
    }

    private void PopulateSelectLists(Asignacion asignacion = null)
    {
        var empleados = _empleadosDbContext.List();
        var proyectos = _proyectosDbContext.List();

        ViewBag.EmpleadoId = new SelectList(empleados, "Id", "Nombre", asignacion?.EmpleadoId);
        ViewBag.ProyectoId = new SelectList(proyectos, "Id", "Titulo", asignacion?.ProyectoId);
    }
}
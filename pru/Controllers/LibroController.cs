using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Negocio.Data;
using Negocio.Models;
using pru.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class LibroController : Controller
{
    private readonly LibreriaDataContext _context;

    public LibroController(LibreriaDataContext context)
    {
        _context = context;
    }

    // Peticion Get para obtener relacion entre las tablas
    [HttpGet]
    public async Task<IActionResult> GetLibros()
    {
        var libros = await _context.Libros
            .Include(l => l.Autor)
            .Select(l => new
            {
                Id = l.Id,
                Titulo = l.Titulo,
                AutorId = l.AutorId,
                NombreAutor = l.Autor.Nombre 
            })
            .ToListAsync();

        return Json(libros);
    }

    // Peticion Get para obtener autores, como refrencia del libro a registar 
    [HttpGet]
    public async Task<IActionResult> GetAutores()
    {
        var autores = await _context.Autores
            .Select(a => new
            {
                AutorId = a.AutorId,
                Nombre = a.Nombre
            })
            .ToListAsync();

        return Json(autores);
    }

    // Peticion Post para añadir la nueva informacion a la base de datos LibreriaData
    [HttpPost]
    public async Task<IActionResult> AddLibro([FromBody] LibroM nuevoLibro)
    {
        if (nuevoLibro == null || string.IsNullOrWhiteSpace(nuevoLibro.Titulo) || nuevoLibro.AutorId <= 0)
        {
            return BadRequest("Datos inválidos");
        }

        var libroEntidad = new Libro
        {
            Titulo = nuevoLibro.Titulo,
            AutorId = nuevoLibro.AutorId
        };

        _context.Libros.Add(libroEntidad);
        await _context.SaveChangesAsync();

        return Json(new { mensaje = "Libro agregado exitosamente" });
    }
}
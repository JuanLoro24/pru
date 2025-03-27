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

    [HttpGet]
    public async Task<IActionResult> GetLibroById(int id)
    {
        var libro = await _context.Libros
            .Where(l => l.Id == id)
            .Select(l => new
            {
                Id = l.Id,
                Titulo = l.Titulo,
                AutorId = l.AutorId
            })
            .FirstOrDefaultAsync();

        if (libro == null)
        {
            return NotFound();
        }

        return Json(libro);
    }

    [HttpPut]
    public async Task<IActionResult> ActualizarLibro(int id, [FromBody] LibroM libroActualizado)
    {
        if (libroActualizado == null || string.IsNullOrWhiteSpace(libroActualizado.Titulo) || libroActualizado.AutorId <= 0)
        {
            return BadRequest("Datos inválidos");
        }

        try
        {
            var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "sp_EditarLibro";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var paramId = command.CreateParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = id;
                command.Parameters.Add(paramId);

                var paramTitulo = command.CreateParameter();
                paramTitulo.ParameterName = "@Titulo";
                paramTitulo.Value = libroActualizado.Titulo;
                command.Parameters.Add(paramTitulo);

                var paramAutorId = command.CreateParameter();
                paramAutorId.ParameterName = "@AutorId";
                paramAutorId.Value = libroActualizado.AutorId;
                command.Parameters.Add(paramAutorId);

                await command.ExecuteNonQueryAsync();
            }

            await connection.CloseAsync();

            return Json(new { mensaje = "Libro actualizado exitosamente usando SP" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = "Error al actualizar el libro", error = ex.Message });
        }
    }

    [HttpDelete]
    public async Task<IActionResult> EliminarLibro(int id)
    {
        var resultado = await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC sp_EliminarLibro {id}");

        if (resultado > 0)
        {
            return Json(new { mensaje = "Libro eliminado correctamente" });
        }
        else
        {
            return NotFound(new { mensaje = "Libro no encontrado" });
        }
    }
}

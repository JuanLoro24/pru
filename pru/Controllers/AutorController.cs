using Microsoft.AspNetCore.Mvc;
using Negocio.Data;
using Negocio.Models;
using pru.Models;
using System.Threading.Tasks;

public class AutorController : Controller
{
    private readonly LibreriaDataContext _context;

    public AutorController(LibreriaDataContext context)
    {
        _context = context;
    }

    //Peticon Post para añadir nuevos autores a la base de datos
    [HttpPost]
    public async Task<IActionResult> AddAutor([FromBody] AutorM nuevoAutor)
    {
        if (nuevoAutor == null || string.IsNullOrWhiteSpace(nuevoAutor.Nombre))
        {
            return BadRequest("Datos inválidos");
        }

        var autorEntidad = new Autore { Nombre = nuevoAutor.Nombre };

        _context.Autores.Add(autorEntidad);
        await _context.SaveChangesAsync();

        var autorViewModel = new AutorM { AutorId = autorEntidad.AutorId, Nombre = autorEntidad.Nombre };

        return Json(autorViewModel);
    }

}
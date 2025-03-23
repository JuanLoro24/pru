using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Negocio.Data; // DbContext
using Negocio.Models; // Modelo de negocio
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pru.Models;

namespace pru.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LibreriaDataContext _context; // DbContext

        public HomeController(ILogger<HomeController> logger, LibreriaDataContext context)
        {
            _logger = logger;
            _context = context;
        }


        // Peticion Get para traer los libros de la base de datos y se muestreb en la interfaz de index
        [HttpGet]
        public async Task<IActionResult> GetLibros()
        {
            var Libros = await _context.Libros
                .Select(a => new LibroM
                {
                    Titulo = a.Titulo

                })
                .ToListAsync();

            return Json(Libros);
        }

        //Control sobre las vistas que se van a presentar en la interfaz grafica
        public IActionResult Index()
        {
            return View(); 
        }
        public IActionResult Autores()
        {
            return View();
        }

        public IActionResult Libros()
        {
            return View(); 
        }
    }
}
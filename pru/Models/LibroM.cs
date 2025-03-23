using Negocio.Models;

namespace pru.Models
{
    public class LibroM
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public int AutorId { get; set; }
        public virtual Autore Autor { get; set; } = null!;
    }
}


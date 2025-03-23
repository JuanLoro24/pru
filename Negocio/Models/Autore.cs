using System;
using System.Collections.Generic;

namespace Negocio.Models;

public partial class Autore
{
    public int AutorId { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}

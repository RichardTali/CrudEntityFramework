using System;
using System.Collections.Generic;

namespace CRUDPRODUCTO.Models;

public partial class Categorium
{
    public int IdCategoria { get; set; }

    public string? NombreCategoria { get; set; }

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
}

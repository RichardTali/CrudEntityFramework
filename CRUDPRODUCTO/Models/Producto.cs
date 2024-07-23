using System;
using System.Collections.Generic;

namespace CRUDPRODUCTO.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? NombreProducto { get; set; }

    public decimal? PrecioProducto { get; set; }

    public int? CantidadProducto { get; set; }

    public int? IdCategoria { get; set; }

    public virtual Categorium? oCategoria { get; set; }
}

using System;
using System.Security.Authentication;

namespace TiendaAccesorios.DTO.Producto.AgregarProducto;

public class AgregarProductoOutput
{
    public Guid IdProducto { get; set; }
    public required string NombreProducto { get; set; }
    public string? Descripcion { get; set; }
    public string? Marca { get; set; }
    public string? Color { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }

    public string? NombreCategoria { get; set; }
    public string? NombreProveedor { get; set; }
}

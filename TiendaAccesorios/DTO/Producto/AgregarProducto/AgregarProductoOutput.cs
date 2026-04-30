using System;
using System.Security.Authentication;

namespace TiendaAccesorios.DTO.Producto.AgregarProducto;

public class AgregarProductoOutput
{
    public Guid IdProducto { get; set; }
    public required string NombreProducto { get; set; }
    public string? Descripcion { get; set; }
    public required string Marca { get; set; }
    public required string Color { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public required string NombreCategoria { get; set; }
}

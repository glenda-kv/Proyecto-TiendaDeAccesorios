using System;
using System.Security.Authentication;

namespace TiendaAccesorios.DTO.Producto.AgregarProducto;

public class AgregarProductoInput
{
    public required string NombreProducto { get; set; }
    public string? Descripcion { get; set; }
    public string? Marca { get; set; }
    public string? Color { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
  

    public Guid IdCategoria { get; set; }
    public Guid IdProveedor { get; set; }
}

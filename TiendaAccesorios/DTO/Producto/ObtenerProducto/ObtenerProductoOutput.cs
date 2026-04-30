using System;

namespace TiendaAccesorios.DTO.Producto.ObtenerProducto;

public class ObtenerProductoOutput
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

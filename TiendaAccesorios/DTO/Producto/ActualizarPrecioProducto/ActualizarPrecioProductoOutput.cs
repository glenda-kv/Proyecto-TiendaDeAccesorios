using System;

namespace TiendaAccesorios.DTO.Producto.ActualizarPrecioProducto;

public class ActualizarPrecioProductoOutput
{
    public Guid IdProducto { get; set; }
    public required string NombreProducto { get; set; }
    public decimal Precio { get; set; }
    public DateTime? FechaActualizacion { get; set; }
}

using System;

namespace TiendaAccesorios.DTO.Producto.CambiarEstadoProducto;

public class CambiarEstadoProductoOutput
{
    public Guid IdProducto { get; set; }
    public required string NombreProducto { get; set; }
    public bool EstaActivo { get; set; }
    public DateTime? FechaActualizacion { get; set; }

}

using System;

namespace TiendaAccesorios.DTO.Producto.IngresoStockProducto;

public class IngresoStockProductoOutput
{
    public Guid IdProducto { get; set; }
    public required string NombreProducto { get; set; }
    public int Stock { get; set; }
    public DateTime? FechaUltimoIngresoStock { get; set; }
}

using System;

namespace TiendaAccesorios.DTO.Producto.StockBajoProductos;

public class StockBajoProductosOutput
{
    public Guid IdProducto { get; set; }
    public required string NombreProducto { get; set; }
    public required string Marca { get; set; }
    public string Categoria { get; set; } = string.Empty;
    public int Stock { get; set; }
}

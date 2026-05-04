using System;

namespace TiendaAccesorios.DTO.Producto.BuscarProductos;

public class BuscarProductosOutput
{
    public Guid IdProducto { get; set; }
    public required string NombreProducto { get; set; }
    public required string Marca { get; set; }
    public required string Color { get; set; }
    public string? Descripcion { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public string Categoria { get; set; } = string.Empty;
}

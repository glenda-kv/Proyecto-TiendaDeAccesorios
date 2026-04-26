using System;

namespace TiendaAccesorios.DTO.Producto.ListarProductos;

public class ListarProductosOutput
{
    public Guid IdProducto { get; set; }
    public string NombreProducto { get; set; }
    public string Marca { get; set; }
    public string Color { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public string NombreCategoria { get; set; }
}

using System;

namespace TiendaAccesorios.DTO.Producto.ActualizarProducto;

public class ActualizarProductoOutput
{
    public Guid IdProducto { get; set; }
    public required string NombreProducto { get; set; }
    public string? Descripcion { get; set; }
    public required string Marca { get; set; }
    public required string Color { get; set; }
    public required string Categoria { get; set; }

}

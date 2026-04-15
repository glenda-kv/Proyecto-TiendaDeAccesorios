using System;

namespace TiendaAccesorios.Entidades;

public class Producto
{ 
    public Guid IdProducto { get; set; }
    public required string NombreProducto { get; set; }
    public string? Descripcion { get; set; }
    public string? Marca { get; set; }
    public string? Color { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public string? Imagen { get; set; }
    public bool Estado { get; set; }
    public DateTime FechaRegistro { get; set; }

    public Guid IdCategoria { get; set; }
    public Categoria? Categoria { get; set; }

    public Guid IdProveedor { get; set; }
    public Proveedor? Proveedor { get; set; }

    public ICollection<DetalleVenta>? DetallesVenta { get; set; }

}

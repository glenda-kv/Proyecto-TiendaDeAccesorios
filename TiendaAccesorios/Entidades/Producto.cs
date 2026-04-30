using System;
using System.Text.Json.Serialization;

namespace TiendaAccesorios.Entidades;

public class Producto
{
    public Guid IdProducto { get; set; }
    public required string NombreProducto { get; set; }
    public string? Descripcion { get; set; }
    public required string Marca { get; set; }
    public required string Color { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public bool EstaActivo { get; set; }
    public DateTime FechaRegistro { get; set; }
    public DateTime? FechaActualizacion { get; set; }
    public DateTime? FechaUltimoIngresoStock { get; set; }
    public Guid IdCategoria { get; set; }

    public Categoria? Categoria { get; set; }

    public ICollection<DetalleVenta> DetallesVenta { get; set; } = new List<DetalleVenta>();

}


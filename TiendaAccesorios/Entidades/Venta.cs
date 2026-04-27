using System;

namespace TiendaAccesorios.Entidades;

public class Venta
{
    public Guid IdVenta { get; set; }
    public DateTime FechaVenta { get; set; }
    public required string MetodoPago { get; set; }
    public decimal Total { get; set; }
    public required string EstadoVenta { get; set; }
    public string? Observacion { get; set; }
    public Guid IdCliente { get; set; }
    public Cliente? Cliente { get; set; }

    public ICollection<DetalleVenta> DetallesVenta { get; set; } = new List<DetalleVenta>();

}

using System;

namespace TiendaAccesorios.Entidades;

public class Venta
{
    public Guid IdVenta { get; set; }
    public DateTime FechaVenta { get; set; }
    public required string FormaDePago { get; set; }
    public decimal Total { get; set; }
    public Guid IdCliente { get; set; }
    public Cliente? Cliente { get; set; }

    public ICollection<DetalleVenta> DetallesVenta { get; set; } = new List<DetalleVenta>();

}

using System;

namespace TiendaAccesorios.Entidades;

public class Venta
{
     public Guid IdVenta { get; set; }
    public DateTime FechaVenta { get; set; }
    public decimal Total { get; set; }
    public required string EstadoVenta { get; set; }

    public Guid IdUsuario { get; set; }
    public Usuario? Usuario { get; set; }

    public ICollection<DetalleVenta>? DetallesVenta { get; set; }
    public ICollection<Pago>? Pagos { get; set; }
}

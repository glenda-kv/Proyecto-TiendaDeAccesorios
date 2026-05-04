using System;

namespace TiendaAccesorios.DTO.Venta.ActualizarObservacionVenta;

public class ActualizarObservacionVentaOutput
{
    public Guid IdVenta { get; set; }
    public required string Cliente { get; set; }
    public string? Observacion { get; set; }
    public DateTime FechaVenta { get; set; }
}

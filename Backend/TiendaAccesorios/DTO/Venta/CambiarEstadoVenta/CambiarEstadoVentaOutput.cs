using System;

namespace TiendaAccesorios.DTO.Venta.CambiarEstadoVenta;

public class CambiarEstadoVentaOutput
{
    public Guid IdVenta { get; set; }
    public required string Cliente { get; set; }
    public required string EstadoVenta { get; set; }
    public DateTime FechaVenta { get; set; }
}

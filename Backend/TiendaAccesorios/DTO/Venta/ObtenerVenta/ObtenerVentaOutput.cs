using System;
using TiendaAccesorios.DTO.Venta.GenerarVenta;

namespace TiendaAccesorios.DTO.Venta.ObtenerVenta;

public class ObtenerVentaOutput
{
    public Guid IdVenta { get; set; }
    public DateTime FechaVenta { get; set; }
    public required string MetodoPago { get; set; }
    public required string EstadoVenta { get; set; }
    public string? Observacion { get; set; }
    public decimal Total { get; set; }
    public required string Cliente { get; set; }
    public string? Telefono { get; set; }
    public List<DetalleVentaOutput> Productos { get; set; } = new();

}

public class DetalleVentaOutput
{
    public required string NombreProducto { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }
}



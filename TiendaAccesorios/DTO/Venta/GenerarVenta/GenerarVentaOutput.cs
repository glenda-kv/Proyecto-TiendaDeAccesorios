using System;

namespace TiendaAccesorios.DTO.Venta.GenerarVenta;

public class GenerarVentaOutput
{
    public Guid IdVenta { get; set; }
    public DateTime FechaVenta { get; set; }
    public required string Cliente { get; set; }
    public required string MetodoPago { get; set; }
    public required string EstadoVenta { get; set; }
    public string? Observacion { get; set; }
    public decimal Total { get; set; }
    public List<GenerarDetalleVentaOutput> Productos { get; set; } = new();
}

public class GenerarDetalleVentaOutput
{
    public required string NombreProducto { get; set; }
    public required string Marca { get; set; }
    public required string Color { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }
}

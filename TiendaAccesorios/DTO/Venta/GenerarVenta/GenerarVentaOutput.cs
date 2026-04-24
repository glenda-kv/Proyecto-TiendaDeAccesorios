using System;

namespace TiendaAccesorios.DTO.Venta;

public class GenerarVentaOutput
{
    public Guid IdVenta { get; set; }
    public DateTime FechaVenta { get; set; }
    public required string FormaDePago { get; set; }
    public decimal Total { get; set; }
    public Guid IdCliente { get; set; }
    public required string NombreCliente { get; set; }

    public List<DetalleVentaSalida> Detalles { get; set; } = new();
}

public class DetalleVentaSalida
{
    public required string NombreProducto { get; set; }
    public required string Marca { get; set; }
    public required string Color { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }
}

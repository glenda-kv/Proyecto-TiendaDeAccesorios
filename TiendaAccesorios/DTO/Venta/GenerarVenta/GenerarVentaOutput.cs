using System;

namespace TiendaAccesorios.DTO.Venta.GenerarVenta;

public class GenerarVentaOutput
{
    public Guid IdVenta { get; set; }
    public DateTime FechaVenta { get; set; }
    public string FormaDePago { get; set; }
    public decimal Total { get; set; }
    public Guid IdCliente { get; set; }
    public string NombreCliente { get; set; }

    public List<DetalleVentaSalida> Detalles { get; set; } = new();
}

public class DetalleVentaSalida
{
    public string NombreProducto { get; set; }
    public string Marca { get; set; }
    public string Color { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }
}

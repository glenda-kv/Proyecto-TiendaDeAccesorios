using System;
using TiendaAccesorios.DTO.Venta.GenerarVenta;

namespace TiendaAccesorios.DTO.Venta.ObtenerVenta;

public class ObtenerVentaOutput
{
    public Guid IdVenta { get; set; }

    public DateTime FechaVenta { get; set; }

    public required string MetodoPago { get; set; }

    public decimal Total { get; set; }

    public required string EstadoVenta { get; set; }

    public string? Observacion { get; set; }

    public Guid IdCliente { get; set; }

    public required string NombreCliente { get; set; }

    public List<DetalleVentaSalida> Detalles { get; set; } = new();

}


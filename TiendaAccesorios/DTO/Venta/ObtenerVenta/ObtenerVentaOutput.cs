using System;
using TiendaAccesorios.DTO.Venta.GenerarVenta;

namespace TiendaAccesorios.DTO.Venta.ObtenerVenta;

public class ObtenerVentaOutput
{
    public Guid IdVenta { get; set; }
    public DateTime FechaVenta { get; set; }
    public string FormaDePago { get; set; }
    public decimal Total { get; set; }
    public Guid IdCliente { get; set; }
    public string NombreCliente { get; set; }

    public List<DetalleVentaSalida> Detalles { get; set; } = new();

}


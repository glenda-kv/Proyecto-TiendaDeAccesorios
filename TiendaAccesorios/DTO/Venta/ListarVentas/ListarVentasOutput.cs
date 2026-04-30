using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaAccesorios.DTO.Venta.ListarVentas;

public class ListarVentasOutput

{
    public Guid IdVenta { get; set; }

    public DateTime FechaVenta { get; set; }

    public required string MetodoPago { get; set; }

    public decimal Total { get; set; }

    public required string EstadoVenta { get; set; }

    public Guid IdCliente { get; set; }

    public required string NombreCliente { get; set; }
}

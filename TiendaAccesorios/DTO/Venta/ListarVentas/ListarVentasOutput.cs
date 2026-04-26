using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaAccesorios.DTO.Venta.ListarVentas;

public class ListarVentasOutput
{
    public Guid IdVenta { get; set; }
    public DateTime FechaVenta { get; set; }
    public string FormaDePago { get; set; }
    public decimal Total { get; set; }
    public Guid IdCliente { get; set; }
    public string NombreCliente { get; set; }
}

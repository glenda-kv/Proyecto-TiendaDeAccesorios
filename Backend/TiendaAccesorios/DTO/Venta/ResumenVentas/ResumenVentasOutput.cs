using System;

namespace TiendaAccesorios.DTO.Venta.ResumenVentas;

public class ResumenVentasOutput
{
    public DateTime Desde { get; set; }
    public DateTime Hasta { get; set; }
    public int TotalVentas { get; set; }
    public decimal TotalIngresos { get; set; }
    public int TotalProductosVendidos { get; set; }
}

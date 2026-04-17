using System;
using System.Text.Json.Serialization;

namespace TiendaAccesorios.Entidades;

public class Pago
{
    public Guid IdPago { get; set; }

    public Guid IdVenta { get; set; }

    [JsonIgnore]
    public Venta? Venta { get; set; }
    public required string MetodoPago { get; set; }
    public decimal MontoPagado { get; set; }
    public decimal SaldoPendiente { get; set; }
    public decimal Interes { get; set; }
    public DateTime FechaPago { get; set; }
    public DateTime? FechaVencimiento { get; set; }
    public required string EstadoPago { get; set; }

}

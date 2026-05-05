using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaAccesorios.DTO.Venta.CambiarEstadoVenta;

public class CambiarEstadoVentaInput
{
    [Required(ErrorMessage = "El estado es obligatorio.")]
    [RegularExpression("(?i)^(Pendiente|Completada|Cancelada)$",
        ErrorMessage = "El estado debe ser Pendiente, Completada o Cancelada.")]
    public required string EstadoVenta { get; set; }
}

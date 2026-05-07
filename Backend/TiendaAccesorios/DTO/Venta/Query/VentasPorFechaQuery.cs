using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaAccesorios.DTO.Venta.Query;

public class VentasPorFechaQuery
{
    [Required(ErrorMessage = "La fecha inicio es obligatoria.")]
    public DateTime Desde { get; set; }

    [Required(ErrorMessage = "La fecha fin es obligatoria.")]
    public DateTime Hasta { get; set; }
}

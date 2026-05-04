using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaAccesorios.DTO.Venta.ActualizarObservacionVenta;

public class ActualizarObservacionVentaInput
{
    [StringLength(500, ErrorMessage = "La observación no puede superar los 500 caracteres.")]
    public string? Observacion { get; set; }
}

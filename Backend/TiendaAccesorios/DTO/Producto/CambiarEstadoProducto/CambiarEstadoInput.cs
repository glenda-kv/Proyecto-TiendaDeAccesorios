using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaAccesorios.DTO.Producto.CambiarEstadoProducto;

public class CambiarEstadoInput
{
    [Required(ErrorMessage = "El estado es obligatorio.")]
    public bool EstaActivo { get; set; }
}

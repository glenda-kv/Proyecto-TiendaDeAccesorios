using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaAccesorios.DTO.Producto.ActualizarPrecioProducto;

public class ActualizarPrecioProductoInput
{
    [Required(ErrorMessage = "El precio es obligatorio.")]
    [Range(0.01, 99999.99, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal Precio { get; set; }
}

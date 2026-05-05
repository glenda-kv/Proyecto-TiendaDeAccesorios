using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaAccesorios.DTO.Producto.IngresoStockProducto;

public class IngresoStockProductoInput
{
    [Required(ErrorMessage = "Las unidades son obligatorias.")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar al menos 1 unidad.")]
    public int Unidades { get; set; }
}

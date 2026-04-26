using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;

namespace TiendaAccesorios.DTO.Producto.AgregarProducto;

public class AgregarProductoInput
{
    [Required(ErrorMessage = "El nombre del producto es obligatorio")]
    public required string NombreProducto { get; set; }
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "La marca es obligatoria")]
    public required string Marca { get; set; }

    [Required(ErrorMessage = "El color es obligatorio")]
    public required string Color { get; set; }

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero")]
    public decimal Precio { get; set; }

    [Required(ErrorMessage = "El stock es obligatorio")]
    [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser un número entero no negativo")]
    public int Stock { get; set; }

    [Required(ErrorMessage = "El ID de la categoría es obligatorio")]
    public Guid IdCategoria { get; set; }
}

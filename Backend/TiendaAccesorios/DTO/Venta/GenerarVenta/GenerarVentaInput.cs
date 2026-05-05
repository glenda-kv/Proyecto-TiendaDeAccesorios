using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaAccesorios.DTO.Venta.GenerarVenta;

public class GenerarVentaInput
{
    [Required(ErrorMessage = "El CI del cliente es obligatorio.")]
    public int Ci { get; set; }

    public string? Complemento { get; set; }

    [Required(ErrorMessage = "El método de pago es obligatorio.")]
    [StringLength(50, ErrorMessage = "El método de pago no puede superar los 50 caracteres.")]
    public required string MetodoPago { get; set; }

    public string? Observacion { get; set; }

    [Required(ErrorMessage = "Debe ingresar al menos un producto.")]
    [MinLength(1, ErrorMessage = "Debe ingresar al menos un producto.")]
    public List<DetalleVentaInput> Productos { get; set; } = new();

}

public class DetalleVentaInput
{
    [Required(ErrorMessage = "El producto es obligatorio.")]
    public Guid IdProducto { get; set; }

    [Required(ErrorMessage = "La cantidad es obligatoria.")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1.")]
    public int Cantidad { get; set; }
}
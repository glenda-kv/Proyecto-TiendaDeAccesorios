using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaAccesorios.DTO.Venta.GenerarVenta;

public class GenerarVentaInput
{
    [Required(ErrorMessage = "El ID del cliente es obligatorio")]

    public Guid IdCliente { get; set; }

    [Required(ErrorMessage = "El método de pago es obligatorio")]

    [MinLength(2, ErrorMessage = "El método de pago es inválido")]

    public required string MetodoPago { get; set; }

    [Required(ErrorMessage = "Debe ingresar al menos un producto")]

    [MinLength(1, ErrorMessage = "La venta debe tener al menos un producto")]

    public List<DetalleVentaInput> Detalles { get; set; } = new();

}

public class DetalleVentaInput

{

    [Required(ErrorMessage = "El nombre del producto es obligatorio")]

    public required string NombreProducto { get; set; }

    [Required(ErrorMessage = "La marca es obligatoria")]

    public required string Marca { get; set; }

    [Required(ErrorMessage = "El color es obligatorio")]

    public required string Color { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]

    public int Cantidad { get; set; }
}
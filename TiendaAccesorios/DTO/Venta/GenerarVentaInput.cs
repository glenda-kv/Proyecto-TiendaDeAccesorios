using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaAccesorios.DTO.Venta;

public class GenerarVentaInput
{
    [Required(ErrorMessage = "El IdUsuario es obligatorio")]
    public Guid IdUsuario { get; set; }

    [Required(ErrorMessage = "La modalidad de pago es obligatoria")]
    [MinLength(3, ErrorMessage = "La modalidad de pago es inválida")]
    public required string ModalidadPago { get; set; }

    [Required(ErrorMessage = "Debe ingresar al menos un producto ")]
    [MinLength(1, ErrorMessage = "La venta debe tener almenos un producto")]
    public List<DetalleVentas> Detalles { get; set; } = new();
}

public class DetalleVentas
{
    [Required(ErrorMessage = "El nombre del producto es obligatorio")]
    public required string NombreProducto { get; set; }

    [Required(ErrorMessage = "La marca  es obligatorio")]
    public required string Marca { get; set; }

    [Required(ErrorMessage = "El color es obligatorio")]
    public required string Color { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
    public int Cantidad { get; set; }
}





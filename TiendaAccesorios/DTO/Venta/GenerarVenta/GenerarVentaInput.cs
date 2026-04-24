using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaAccesorios.DTO.Venta.GenerarVenta;

public class GenerarVentaInput
{
    [Required(ErrorMessage = "El IdCliente es obligatorio")]
    public Guid IdCliente { get; set; }

    [Required(ErrorMessage = "La forma de pago es obligatoria")]
    [MinLength(2, ErrorMessage = "La forma de pago es inválida")]
    public required string FormaDePago { get; set; }

    [Required(ErrorMessage = "Debe ingresar al menos un producto")]
    [MinLength(1, ErrorMessage = "La venta debe tener al menos un producto")]
    public List<DetalleVentas> Detalles { get; set; } = new List<DetalleVentas>();
}

public class DetalleVentas
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





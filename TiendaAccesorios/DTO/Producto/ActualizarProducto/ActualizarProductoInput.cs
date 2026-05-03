using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaAccesorios.DTO.Producto.ActualizarProducto;

public class ActualizarProductoInput
{
    [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
    public required string NombreProducto { get; set; }

    [StringLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres.")]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "La marca es obligatoria.")]
    [StringLength(50, ErrorMessage = "La marca no puede superar los 50 caracteres.")]
    public required string Marca { get; set; }

    [Required(ErrorMessage = "El color es obligatorio.")]
    [StringLength(30, ErrorMessage = "El color no puede superar los 30 caracteres.")]
    public required string Color { get; set; }

    [Required(ErrorMessage = "La categoría es obligatoria.")]
    [StringLength(50, ErrorMessage = "El nombre de la categoría no puede superar los 50 caracteres.")]
    public required string Categoria { get; set; }
}


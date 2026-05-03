using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;

namespace TiendaAccesorios.DTO.Producto.AgregarProducto;

public class AgregarProductoInput
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

    [Required(ErrorMessage = "El precio es obligatorio.")]
    [Range(0.01, 99999.99, ErrorMessage = "El precio debe ser mayor a 0.")]
    public decimal Precio { get; set; }

    [Required(ErrorMessage = "El stock es obligatorio.")]
    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
    public int Stock { get; set; }

    [Required(ErrorMessage = "La categoría es obligatoria.")]
    [StringLength(50, ErrorMessage = "El nombre de la categoría no puede superar los 50 caracteres.")]
    public required string Categoria { get; set; }
}


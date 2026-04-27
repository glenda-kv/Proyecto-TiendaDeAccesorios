using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaAccesorios.DTO.Categoria.ActualizarCategoria;

public class ActualizarCategoriaInput
{
    [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
    public required string NombreCategoria { get; set; }

    [StringLength(250, ErrorMessage = "La descripción no puede superar los 250 caracteres.")]
    public string? Descripcion { get; set; }
}

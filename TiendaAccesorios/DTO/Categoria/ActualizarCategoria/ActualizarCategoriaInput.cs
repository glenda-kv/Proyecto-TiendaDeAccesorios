using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaAccesorios.DTO.Categoria.ActualizarCategoria;

public class ActualizarCategoriaInput
{
    [Required(ErrorMessage = "El nombre de la categoria es obligatorio")]
    public required string NombreCategoria { get; set; }
    public bool Estado { get; set; }
}

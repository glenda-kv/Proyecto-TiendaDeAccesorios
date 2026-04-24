using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaAccesorios.DTO.Categoria.AgregarCategoria;

public class AgregarCategoriaInput
{
    [Required(ErrorMessage = "El nombre de la categoria es obligatorio")]
    public required string NombreCompleto { get; set; }
}

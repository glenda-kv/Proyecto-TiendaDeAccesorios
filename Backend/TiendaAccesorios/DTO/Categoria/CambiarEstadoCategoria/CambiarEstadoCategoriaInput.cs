using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaAccesorios.DTO.Categoria.CambiarEstadoCategoria;

public class CambiarEstadoCategoriaInput
{
    [Required(ErrorMessage = "El estado es obligatorio.")]
    public bool? EstaActivo { get; set; }
}

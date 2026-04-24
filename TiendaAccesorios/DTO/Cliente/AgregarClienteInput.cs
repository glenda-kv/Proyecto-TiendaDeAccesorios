using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.OpenApi.MicrosoftExtensions;

namespace TiendaAccesorios.DTO.Cliente;

public class AgregarClienteInput
{
    [Required(ErrorMessage = "El CI es obligatorio")]
    public int Ci { get; set; }
    public string? Extension { get; set; }

    [Required(ErrorMessage = "El nombre completo es obligatorio")]
    public required string NombreCompleto { get; set; }
    public string? Telefono { get; set; }

}

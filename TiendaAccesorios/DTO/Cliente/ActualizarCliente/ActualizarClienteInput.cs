using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaAccesorios.DTO.Cliente.ActualizarCliente;

public class ActualizarClienteInput
{
    [Required(ErrorMessage = "El CI es obligatorio")]
    public int Ci { get; set; }

    [MaxLength(2, ErrorMessage = "La extensión no debe superar los 2 caracteres")]
    public string? Extension { get; set; }

    [Required(ErrorMessage = "El nombre completo es obligatorio")]
    public required string NombreCompleto { get; set; }

    public string? Telefono { get; set; }

    public bool Estado { get; set; }
}

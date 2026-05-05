using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaAccesorios.DTO.Cliente.ActualizarCliente;

public class ActualizarClienteInput
{
    [Required(ErrorMessage = "El CI es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "El CI debe ser mayor a 0.")]
    public int Ci { get; set; }

    [StringLength(2, ErrorMessage = "El complemento no puede superar los 2 caracteres.")]
    public string? Complemento { get; set; }

    [Required(ErrorMessage = "El nombre completo es obligatorio.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
    public required string NombreCompleto { get; set; }

    [StringLength(15, ErrorMessage = "El teléfono no puede superar los 15 caracteres.")]
    public string? Telefono { get; set; }

    [StringLength(100, ErrorMessage = "El correo no puede superar los 100 caracteres.")]
    public string? Correo { get; set; }
}

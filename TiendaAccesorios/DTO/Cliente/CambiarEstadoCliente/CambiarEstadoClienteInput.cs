using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaAccesorios.DTO.Cliente.CambiarEstadoCliente;

public class CambiarEstadoClienteInput
{
    [Required(ErrorMessage = "El estado es obligatorio.")]
    public bool? EstaActivo { get; set; }
}

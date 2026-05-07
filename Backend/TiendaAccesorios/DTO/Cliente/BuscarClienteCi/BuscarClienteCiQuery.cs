using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaAccesorios.DTO.Cliente.BuscarClienteCi;

public class BuscarClienteCiQuery
{
    [Required(ErrorMessage = "El CI es obligatorio.")]
    public int Ci { get; set; }
    public string? Complemento { get; set; }
}

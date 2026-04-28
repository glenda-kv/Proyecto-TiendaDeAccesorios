using System;

namespace TiendaAccesorios.DTO.Cliente.CambiarEstadoCliente;

public class CambiarEstadoClienteOutput
{
    public Guid IdCliente { get; set; }
    public required string NombreCompleto { get; set; }
    public bool EstaActivo { get; set; }
}

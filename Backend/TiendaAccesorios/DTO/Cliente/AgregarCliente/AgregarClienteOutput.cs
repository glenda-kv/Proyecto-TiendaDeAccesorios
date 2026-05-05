using System;

namespace TiendaAccesorios.DTO.Cliente.AgregarCliente;

public class AgregarClienteOutput
{
    public Guid IdCliente { get; set; }
    public int Ci { get; set; }
    public string? Complemento { get; set; }
    public required string NombreCompleto { get; set; }
    public string? Telefono { get; set; }
    public string? Correo { get; set; }
    public DateTime FechaRegistro { get; set; }

}

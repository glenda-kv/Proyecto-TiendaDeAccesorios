using System;

namespace TiendaAccesorios.DTO.Cliente.ListarClientes;

public class ListarClientesOutput
{
    public Guid IdCliente { get; set; }
    public int Ci { get; set; }
    public string? Complemento { get; set; }
    public required string NombreCompleto { get; set; }
    public string? Telefono { get; set; }
    public bool EstaActivo { get; set; }
}

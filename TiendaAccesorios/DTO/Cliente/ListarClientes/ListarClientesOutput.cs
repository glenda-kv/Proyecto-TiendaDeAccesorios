using System;

namespace TiendaAccesorios.DTO.Cliente.ListarClientes;

public class ListarClientesOutput
{
    public Guid IdCliente { get; set; }
    public int Ci { get; set; }
    public string? Extension { get; set; }
    public string NombreCompleto { get; set; }
    public string? Telefono { get; set; }
    public bool Estado { get; set; }
    public DateTime FechaRegistro { get; set; }
}

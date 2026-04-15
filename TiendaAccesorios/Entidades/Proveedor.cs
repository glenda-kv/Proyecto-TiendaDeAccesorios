using System;

namespace TiendaAccesorios.Entidades;

public class Proveedor
{
    public Guid IdProveedor { get; set; }
    public required string NombreCompleto { get; set; }
    public required string Telefono { get; set; }
    public string? Correo { get; set; }
    public string? Direccion { get; set; }
    public bool Estado { get; set; }
    public DateTime FechaRegistro { get; set; }

    public ICollection<Producto>? Productos { get; set; }
}


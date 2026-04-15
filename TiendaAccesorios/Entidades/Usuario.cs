using System;

namespace TiendaAccesorios.Entidades;

public class Usuario
{
    public Guid IdUsuario { get; set; }
    public required string NombreCompleto { get; set; }
    public required string Correo { get; set; }
    public required string Contrasenia { get; set; }
    public string? Telefono { get; set; }
    public required string Rol { get; set; }
    public bool Estado { get; set; }
    public DateTime FechaRegistro { get; set; }

    public ICollection<Venta>? Ventas { get; set; }

}

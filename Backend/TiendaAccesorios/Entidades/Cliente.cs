using System;
using System.Text.Json.Serialization;

namespace TiendaAccesorios.Entidades;

public class Cliente
{
    public Guid IdCliente { get; set; }
     public int Ci { get; set; }
    public string? Complemento { get; set; }
    public required string NombreCompleto { get; set; }
    public string? Telefono { get; set; }
    public string? Correo { get; set; }
    public bool EstaActivo { get; set; }
    public DateTime FechaRegistro { get; set; }
    public DateTime? FechaActualizacion { get; set; }

    public ICollection<Venta>? Ventas { get; set; }
}

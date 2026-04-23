using System;
using System.Text.Json.Serialization;

namespace TiendaAccesorios.Entidades;

public class Cliente
{
    public Guid IdCliente { get; set; }
     public int Ci { get; set; }
    public string? Extension { get; set; }
    public required string NombreCompleto { get; set; }
    public string? Telefono { get; set; }
    public bool Estado { get; set; }
    public DateTime FechaRegistro { get; set; }

    [JsonIgnore]
    public ICollection<Venta>? Ventas { get; set; }
}

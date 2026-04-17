using System;
using System.Text.Json.Serialization;

namespace TiendaAccesorios.Entidades;

public class Categoria
{
    public Guid IdCategoria { get; set; }
    public required string NombreCategoria { get; set; }
    public string? Descripcion { get; set; }
    public bool Estado { get; set; }

    [JsonIgnore]
    public ICollection<Producto>? Productos { get; set; }
}

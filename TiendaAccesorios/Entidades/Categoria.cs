using System;

namespace TiendaAccesorios.Entidades;

public class Categoria
{
    public Guid IdCategoria { get; set; }
    public required string NombreCategoria { get; set; }
    public string? Descripcion { get; set; }
    public bool Estado { get; set; }

    public ICollection<Producto>? Productos { get; set; }
}

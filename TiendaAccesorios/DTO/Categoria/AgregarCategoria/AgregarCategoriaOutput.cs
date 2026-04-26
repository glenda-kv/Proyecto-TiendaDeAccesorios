using System;

namespace TiendaAccesorios.DTO.Categoria.AgregarCategoria;

public class AgregarCategoriaOutput
{
    public Guid IdCategoria { get; set; }
    public required string NombreCategoria { get; set; }
    public bool Estado { get; set; }
}

using System;

namespace TiendaAccesorios.DTO.Categoria.ListarCategorias;

public class ListarCategoriasOutput
{
    public Guid IdCategoria { get; set; }
    public required string NombreCategoria { get; set; }
    public string? Descripcion { get; set; } 
    public bool EstaActivo { get; set; }
}

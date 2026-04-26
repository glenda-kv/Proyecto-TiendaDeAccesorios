using System;

namespace TiendaAccesorios.DTO.Categoria.ListarCategorias;

public class ListarCategoriasOutput
{
    public Guid IdCategoria { get; set; }
    public string NombreCategoria { get; set; }
    public bool Estado { get; set; }
}

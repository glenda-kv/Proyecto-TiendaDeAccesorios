using System;

namespace TiendaAccesorios.DTO.Categoria.ActualizarCategoria;

public class ActualizarCategoriaOutput
{
    public Guid IdCategoria { get; set; }
    public string NombreCategoria { get; set; }
    public bool Estado { get; set; }
}

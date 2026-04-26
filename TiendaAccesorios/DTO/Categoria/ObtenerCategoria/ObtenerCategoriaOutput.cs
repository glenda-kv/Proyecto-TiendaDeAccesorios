using System;

namespace TiendaAccesorios.DTO.Categoria.ObtenerCategoria;

public class ObtenerCategoriaOutput
{
    public Guid IdCategoria { get; set; }
    public string NombreCategoria { get; set; }
    public bool Estado { get; set; }
}

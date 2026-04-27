using System;

namespace TiendaAccesorios.DTO.Categoria.CambiarEstadoCategoria;

public class CambiarEstadoCategoriaOutput
{
    public Guid IdCategoria { get; set; }
    public required string NombreCategoria { get; set; }
    public bool EstaActivo { get; set; }

}

using System;

namespace TiendaAccesorios.DTO.Venta.Query;

public class ListarVentasPorClienteQuery
{
    public int? Ci { get; set; }
    public string? Nombre { get; set; }
}

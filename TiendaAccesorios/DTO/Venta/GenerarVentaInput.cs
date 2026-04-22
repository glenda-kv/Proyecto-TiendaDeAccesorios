using System;

namespace TiendaAccesorios.DTO.Venta;

public class GenerarVentaInput
{

    public Guid IdUsuario { get; set; }
    public required string ModalidadPago { get; set; }

    public List<DetalleVentas> Detalles { get; set; } = new();
}

public class DetalleVentas
{
    public required string NombreProducto { get; set; }
    public required string Marca { get; set; }
    public required string Color { get; set; }
    public int Cantidad { get; set; }
}





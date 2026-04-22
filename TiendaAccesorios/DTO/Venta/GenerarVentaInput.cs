using System;

namespace TiendaAccesorios.DTO.Venta;

public class GenerarVentaInput
{

    public Guid IdUsuario { get; set; }
    public required string ModalidadPago { get; set; }

    public List<DetalleVenta> Detalles { get; set; } = new();
}

public class DetalleVenta
{
    public required string NombreProducto { get; set; }
    public string? Marca { get; set; }
    public string? Color { get; set; }
    public int Cantidad { get; set; }
}





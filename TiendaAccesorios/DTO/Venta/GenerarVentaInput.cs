using System;

namespace TiendaAccesorios.DTO.Venta;

public class GenerarVentaInput
{
    public Guid IdCliente { get; set; }
    public required string ModalidadPago { get; set; }

    public List<ProductosDeEntrada> Detalle { get; set; } = new List<ProductosDeEntrada>();
}

public class ProductosDeEntrada
{
    public required string NombreProducto { get; set; }
    public required string Marca { get; set; }
    public required string Color { get; set; }
    public int Cantidad { get; set; }
}





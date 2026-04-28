using System;

namespace TiendaAccesorios.DTO.Cliente.HistorialComprasCliente;

public class HistorialComprasPorClienteOutput
{

    public Guid IdVenta { get; set; }
    public DateTime FechaVenta { get; set; }
    public required string MetodoPago { get; set; }
    public required string EstadoVenta { get; set; }
    public string? Observacion { get; set; }
    public decimal Total { get; set; }
    public List<DetalleOutput> Detalles { get; set; } = new List<DetalleOutput>();


}

public class DetalleOutput
{
        public Guid IdProducto { get; set; }
        public required string NombreProducto { get; set; }
        public required string Marca { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
}

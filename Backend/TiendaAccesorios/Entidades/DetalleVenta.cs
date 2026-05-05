using System;

namespace TiendaAccesorios.Entidades;

public class DetalleVenta
{
     public Guid IdDetalleVenta { get; set; }

    public Guid IdVenta { get; set; }
    public Venta? Venta { get; set; }

    public Guid IdProducto { get; set; }
    public Producto? Producto { get; set; }

    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }
}



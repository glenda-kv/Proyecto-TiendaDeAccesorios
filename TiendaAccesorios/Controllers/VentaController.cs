using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaAccesorios.Data;
using TiendaAccesorios.DTO.Venta.GenerarVenta;
using TiendaAccesorios.Entidades;

namespace TiendaAccesorios.Controllers;

public class VentaController : BaseApiController
{
    private readonly AppDbContext _context;

    public VentaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<GenerarVentaOutput>> GenerarVenta([FromBody] GenerarVentaInput entrada)
    {
        decimal total = 0;

        var cliente = await _context.Clientes.FindAsync(entrada.IdCliente);

        if (cliente == null)
            return BadRequest("El cliente no existe");

        var venta = new Venta
        {
            IdVenta = Guid.NewGuid(),
            FechaVenta = DateTime.UtcNow,
            FormaDePago = entrada.FormaDePago,
            IdCliente = entrada.IdCliente,
            DetallesVenta = new List<DetalleVenta>()
        };
        
        var detallesSalida = new List<DetalleVentaSalida>();

        foreach (var item in entrada.Detalles)
        {
            var producto = await _context.Productos.FirstOrDefaultAsync(p =>
                p.NombreProducto == item.NombreProducto &&
                p.Marca == item.Marca &&
                p.Color == item.Color
            );

            if (producto == null)
                return BadRequest($"Producto no encontrado: {item.NombreProducto}");

            if (producto.Stock < item.Cantidad)
                return BadRequest($"Stock insuficiente para {producto.NombreProducto}");

            producto.Stock -= item.Cantidad;

            var subtotal = producto.Precio * item.Cantidad;

            venta.DetallesVenta.Add(new DetalleVenta
            {
                IdDetalleVenta = Guid.NewGuid(),
                IdVenta = venta.IdVenta,
                IdProducto = producto.IdProducto,
                Cantidad = item.Cantidad,
                PrecioUnitario = producto.Precio,
                Subtotal = subtotal
            });

            detallesSalida.Add(new DetalleVentaSalida
            {
                NombreProducto = producto.NombreProducto,
                Marca = producto.Marca,
                Color = producto.Color,
                Cantidad = item.Cantidad,
                PrecioUnitario = producto.Precio,
                Subtotal = subtotal
            });

            total += subtotal;
        }

        venta.Total = total;

        _context.Ventas.Add(venta);
        await _context.SaveChangesAsync();

        var salida = new GenerarVentaOutput
        {
            IdVenta = venta.IdVenta,
            FechaVenta = venta.FechaVenta,
            FormaDePago = venta.FormaDePago,
            Total = venta.Total,
            IdCliente = cliente.IdCliente,
            NombreCliente = cliente.NombreCompleto,
            Detalles = detallesSalida
        };

        return CreatedAtAction(nameof(GenerarVenta), new { id = venta.IdVenta }, salida);
    }
}

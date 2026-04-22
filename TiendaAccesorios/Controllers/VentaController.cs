using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaAccesorios.Data;
using TiendaAccesorios.DTO.Venta;
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
    public async Task<ActionResult<Venta>> GenerarVenta(GenerarVentaInput entrada)
    {
        decimal total = 0;

        var venta = new Venta
        {
            IdVenta = Guid.NewGuid(),
            FechaVenta = DateTime.UtcNow,
            ModalidadPago = entrada.ModalidadPago,
            EstadoVenta = "COMPLETADO",
            IdUsuario = entrada.IdUsuario,
            DetallesVenta = new List<DetalleVenta>()
        };

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
                IdProducto = producto.IdProducto,
                Cantidad = item.Cantidad,
                PrecioUnitario = producto.Precio,
                Subtotal = subtotal
            });

            total += subtotal;
        }

        venta.Total = total;

        _context.Ventas.Add(venta);
        await _context.SaveChangesAsync();

        return Ok(venta);
    }
}

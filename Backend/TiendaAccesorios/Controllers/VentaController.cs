using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaAccesorios.Data;
using TiendaAccesorios.DTO.Venta.ActualizarObservacionVenta;
using TiendaAccesorios.DTO.Venta.CambiarEstadoVenta;
using TiendaAccesorios.DTO.Venta.GenerarVenta;
using TiendaAccesorios.DTO.Venta.ListarVentas;
using TiendaAccesorios.DTO.Venta.ObtenerVenta;
using TiendaAccesorios.DTO.Venta.Query;
using TiendaAccesorios.DTO.Venta.ResumenVentas;
using TiendaAccesorios.Entidades;

namespace TiendaAccesorios.Controllers;

public class VentaController : BaseApiController
{
    private readonly AppDbContext _contexto;
    private readonly IMapper _mapper;

    public VentaController(AppDbContext contexto, IMapper mapper)
    {
        _contexto = contexto;
        _mapper = mapper;
    }
    
    [HttpGet("ListarVentas")]
    [ActionName("ListarVentas")]
    public async Task<ActionResult<ICollection<ListarVentasOutput>>> ListarVentas()
    {
        var ventas = await _contexto.Ventas
            .OrderByDescending(v => v.FechaVenta)
            .ProjectTo<ListarVentasOutput>(_mapper.ConfigurationProvider)
            .ToListAsync();

        if (!ventas.Any())
            return NotFound(new { mensaje = "No hay ventas registradas." });

        return Ok(ventas);
    }

    [HttpGet("{id:guid}/obtener-venta")]
    [ActionName("ObtenerVenta")]
    public async Task<ActionResult<ObtenerVentaOutput>> ObtenerVenta(Guid id)
    {
        var venta = await _contexto.Ventas
            .Where(v => v.IdVenta == id)
            .ProjectTo<ObtenerVentaOutput>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (venta is null)
            return NotFound(new { mensaje = "Venta no encontrada." });

        return Ok(venta);
    }


    [HttpPost("generar-venta")]
    [ActionName("GenerarVenta")]
    public async Task<ActionResult<GenerarVentaOutput>> GenerarVenta([FromBody] GenerarVentaInput entrada)
    {
        var cliente = await _contexto.Clientes
            .FirstOrDefaultAsync(c => c.Ci == entrada.Ci &&
                                    c.Complemento == entrada.Complemento &&
                                    c.EstaActivo);

        if (cliente is null)
            return NotFound(new { mensaje = "Cliente no encontrado." });

        var idsProductos = entrada.Productos.Select(p => p.IdProducto).ToList();

        var hayDuplicados = idsProductos.Any(id => idsProductos.Count(x => x == id) > 1);
        if (hayDuplicados)
            return BadRequest(new { mensaje = "No puede ingresar el mismo producto más de una vez." });

        var productos = await _contexto.Productos
            .Where(p => idsProductos.Contains(p.IdProducto) && p.EstaActivo)
            .ToListAsync();

        var hayProductosNoEncontrados = idsProductos.Any(id => !productos.Any(p => p.IdProducto == id));
        if (hayProductosNoEncontrados)
            return NotFound(new { mensaje = "Uno o más productos no fueron encontrados." });

        foreach (var detalle in entrada.Productos)
        {
            var producto = productos.First(p => p.IdProducto == detalle.IdProducto);

            if (producto.Stock < detalle.Cantidad)
                return BadRequest(new
                {
                    mensaje = $"Stock insuficiente para {producto.NombreProducto}. Stock disponible: {producto.Stock}."
                });
        }

        var detalles = entrada.Productos.Select(detalle =>
        {
            var producto = productos.First(p => p.IdProducto == detalle.IdProducto);

            producto.Stock -= detalle.Cantidad;
            producto.FechaActualizacion = DateTime.UtcNow;

            return new DetalleVenta
            {
                IdDetalleVenta = Guid.NewGuid(),
                IdProducto = producto.IdProducto,
                Producto = producto,
                Cantidad = detalle.Cantidad,
                PrecioUnitario = producto.Precio,
                Subtotal = detalle.Cantidad * producto.Precio
            };
        }).ToList();

        var venta = new Venta
        {
            IdVenta = Guid.NewGuid(),
            FechaVenta = DateTime.UtcNow,
            MetodoPago = entrada.MetodoPago,
            EstadoVenta = "Completada",
            Observacion = entrada.Observacion,
            IdCliente = cliente.IdCliente,
            Cliente = cliente,
            Total = detalles.Sum(d => d.Subtotal),
            DetallesVenta = detalles
        };

        _contexto.Ventas.Add(venta);
        await _contexto.SaveChangesAsync();

        var salida = _mapper.Map<GenerarVentaOutput>(venta);
        return CreatedAtAction(nameof(ObtenerVenta), new { id = venta.IdVenta }, salida);
    }

    [HttpPatch("{id:guid}/estado")]
    [ActionName("CambiarEstadoVenta")]
    public async Task<ActionResult<CambiarEstadoVentaOutput>> ActualizarEstadoVenta(Guid id, [FromBody] CambiarEstadoVentaInput entrada)
    {
        var venta = await _contexto.Ventas
            .Include(v => v.Cliente)
            .FirstOrDefaultAsync(v => v.IdVenta == id);

        if (venta is null)
            return NotFound(new { mensaje = "Venta no encontrada." });

        venta.EstadoVenta = char.ToUpper(entrada.EstadoVenta[0]) +
                            entrada.EstadoVenta[1..].ToLower();

        await _contexto.SaveChangesAsync();

        var salida = _mapper.Map<CambiarEstadoVentaOutput>(venta);
        return Ok(salida);
    }

    [HttpPatch("{id:guid}/observacion")]
    [ActionName("ActualizarObservacionVenta")]
    public async Task<ActionResult<ActualizarObservacionVentaOutput>> ActualizarObservacionVenta(Guid id, [FromBody] ActualizarObservacionVentaInput entrada)
    {
        var venta = await _contexto.Ventas
            .Include(v => v.Cliente)
            .FirstOrDefaultAsync(v => v.IdVenta == id);

        if (venta is null)
            return NotFound(new { mensaje = "Venta no encontrada." });

        venta.Observacion = entrada.Observacion;

        await _contexto.SaveChangesAsync();

        var salida = _mapper.Map<ActualizarObservacionVentaOutput>(venta);
        return Ok(salida);
    }


    [HttpGet("listar-ventas-por-cliente")]
    [ActionName("ListarVentasPorCliente")]
    public async Task<ActionResult<ICollection<ObtenerVentaOutput>>> ListarVentasPorCliente(
        [FromQuery] ListarVentasPorClienteQuery entrada)
    {
        if (entrada.Ci is null && entrada.Nombre is null)
            return BadRequest(new { mensaje = "Debe ingresar al menos el CI o el nombre del cliente." });

        var ventas = await _contexto.Ventas
            .Where(v => (entrada.Ci == null || v.Cliente!.Ci == entrada.Ci) &&
                        (entrada.Nombre == null || v.Cliente!.NombreCompleto.Contains(entrada.Nombre)))
            .OrderByDescending(v => v.FechaVenta)
            .ProjectTo<ObtenerVentaOutput>(_mapper.ConfigurationProvider)
            .ToListAsync();

        if (!ventas.Any())
            return NotFound(new { mensaje = "No se encontraron ventas para ese cliente." });

        return Ok(ventas);
    }


    [HttpGet("ventas-por-estado")]
    [ActionName("ListarVentasPorEstado")]
    public async Task<ActionResult<ICollection<ObtenerVentaOutput>>> ListarVentasPorEstado(
        [FromQuery] string estado)
    {
        var ventas = await _contexto.Ventas
            .Where(v => v.EstadoVenta.ToLower() == estado.ToLower())
            .OrderByDescending(v => v.FechaVenta)
            .ProjectTo<ObtenerVentaOutput>(_mapper.ConfigurationProvider)
            .ToListAsync();

        if (!ventas.Any())
            return NotFound(new { mensaje = "No se encontraron ventas con ese estado." });

        return Ok(ventas);
    }

    [HttpGet("ventas-por-fecha")]
    [ActionName("ListarVentasPorFecha")]
    public async Task<ActionResult<ICollection<ObtenerVentaOutput>>> ListarVentasPorFecha(
        [FromQuery] VentasPorFechaQuery entrada)
    {
        if (entrada.Desde > entrada.Hasta)
            return BadRequest(new { mensaje = "La fecha de inicio no puede ser mayor a la fecha final." });

        var ventas = await _contexto.Ventas
            .Where(v => v.FechaVenta.Date >= entrada.Desde.Date &&
                        v.FechaVenta.Date <= entrada.Hasta.Date)
            .OrderByDescending(v => v.FechaVenta)
            .ProjectTo<ObtenerVentaOutput>(_mapper.ConfigurationProvider)
            .ToListAsync();

        if (!ventas.Any())
            return NotFound(new { mensaje = "No se encontraron ventas en ese rango de fechas." });

        return Ok(ventas);
    }

    [HttpGet("ventas-por-metodo-pago")]
    [ActionName("ListarVentasPorMetodoPago")]
    public async Task<ActionResult<ICollection<ListarVentasOutput>>> ListarVentasPorMetodoPago(
        [FromQuery] string metodoPago)
    {
        var ventas = await _contexto.Ventas
            .Where(v => v.MetodoPago.ToLower() == metodoPago.ToLower())
            .OrderByDescending(v => v.FechaVenta)
            .ProjectTo<ListarVentasOutput>(_mapper.ConfigurationProvider)
            .ToListAsync();

        if (!ventas.Any())
            return NotFound(new { mensaje = "No se encontraron ventas con ese método de pago." });

        return Ok(ventas);
    }

    [HttpGet("resumen-ventas-por-fecha")]
    [ActionName("ResumenVentas")]
    public async Task<ActionResult<ResumenVentasOutput>> ResumenVentas(
        [FromQuery] VentasPorFechaQuery entrada)
    {
        if (entrada.Desde > entrada.Hasta)
            return BadRequest(new { mensaje = "La fecha inicio no puede ser mayor a la fecha fin." });

        var ventas = await _contexto.Ventas
            .Include(v => v.DetallesVenta)
            .Where(v => v.FechaVenta.Date >= entrada.Desde.Date &&
                        v.FechaVenta.Date <= entrada.Hasta.Date)
            .ToListAsync();

        if (!ventas.Any())
            return NotFound(new { mensaje = "No se encontraron ventas en ese rango de fechas." });

        var salida = new ResumenVentasOutput
        {
            Desde = entrada.Desde,
            Hasta = entrada.Hasta,
            TotalVentas = ventas.Count,
            TotalIngresos = ventas.Sum(v => v.Total),
            TotalProductosVendidos = ventas.Sum(v => v.DetallesVenta.Sum(d => d.Cantidad))
        };

        return Ok(salida);
    }

    
}

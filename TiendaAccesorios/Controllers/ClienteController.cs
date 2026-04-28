using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaAccesorios.Data;
using TiendaAccesorios.DTO.Cliente.ActualizarCliente;
using TiendaAccesorios.DTO.Cliente.AgregarCliente;
using TiendaAccesorios.DTO.Cliente.BuscarClienteCi;
using TiendaAccesorios.DTO.Cliente.BuscarClientePorNombre;
using TiendaAccesorios.DTO.Cliente.CambiarEstadoCliente;
using TiendaAccesorios.DTO.Cliente.HistorialComprasCliente;
using TiendaAccesorios.DTO.Cliente.ListarClientes;
using TiendaAccesorios.DTO.Cliente.ObtenerCliente;
using TiendaAccesorios.Entidades;

namespace TiendaAccesorios.Controllers;

public class ClienteController : BaseApiController
{
    private readonly AppDbContext _contexto;

    public ClienteController(AppDbContext contexto)
    {
        _contexto = contexto;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<ListarClientesOutput>>> GetClientes()
    {
        var clientes = await _contexto.Clientes
            .AsNoTracking()
            .Where(x => x.EstaActivo)  
            .Select(x => new ListarClientesOutput
            {
                IdCliente      = x.IdCliente,
                Ci             = x.Ci,
                Complemento    = x.Complemento,
                NombreCompleto = x.NombreCompleto,
                Telefono       = x.Telefono,
                EstaActivo     = x.EstaActivo
            })
            .ToListAsync();

        return Ok(clientes);
    }

    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ObtenerClienteOutput>> GetCliente(Guid id)
    {
        var cliente = await _contexto.Clientes
            .AsNoTracking()
            .Where(x => x.IdCliente == id)
            .Select(x => new ObtenerClienteOutput
            {
                IdCliente          = x.IdCliente,
                Ci                 = x.Ci,
                Complemento        = x.Complemento,
                NombreCompleto     = x.NombreCompleto,
                Telefono           = x.Telefono,
                Correo             = x.Correo,
                EstaActivo         = x.EstaActivo,
                FechaRegistro      = x.FechaRegistro,
                FechaActualizacion = x.FechaActualizacion
            })
            .FirstOrDefaultAsync();

        if (cliente is null)
            return NotFound($"No se encontró el cliente con ID {id}.");

        return Ok(cliente);
    }


    [HttpPost]
    public async Task<ActionResult<AgregarClienteOutput>> CreateCliente([FromBody] AgregarClienteInput entrada)
    {
        var complementoNormalizado = entrada.Complemento?.Trim().ToUpper();

        var existeCliente = await _contexto.Clientes
            .AnyAsync(x =>
                x.Ci == entrada.Ci &&
                (x.Complemento ?? string.Empty).Trim().ToUpper() == (complementoNormalizado ?? string.Empty));

        if (existeCliente)
            return Conflict($"Ya existe un cliente con el CI {entrada.Ci} {complementoNormalizado}.");

        var nuevoCliente = new Cliente
        {
            Ci             = entrada.Ci,
            Complemento    = complementoNormalizado,
            NombreCompleto = entrada.NombreCompleto.Trim(),
            Telefono       = entrada.Telefono?.Trim(),
            Correo         = entrada.Correo?.Trim(),
            EstaActivo     = true,
            FechaRegistro  = DateTime.UtcNow
        };

        _contexto.Clientes.Add(nuevoCliente);
        await _contexto.SaveChangesAsync();

        var salida = new AgregarClienteOutput
        {
            IdCliente      = nuevoCliente.IdCliente,
            Ci             = nuevoCliente.Ci,
            Complemento    = nuevoCliente.Complemento,
            NombreCompleto = nuevoCliente.NombreCompleto,
            Telefono       = nuevoCliente.Telefono,
            Correo         = nuevoCliente.Correo,
            FechaRegistro  = nuevoCliente.FechaRegistro
        };

        return CreatedAtAction(nameof(GetCliente), new { id = salida.IdCliente }, salida);
    }


    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ActualizarClienteOutput>> UpdateCliente(Guid id, [FromBody] ActualizarClienteInput entrada)
    {
        var complementoNormalizado = entrada.Complemento?.Trim().ToUpper();

        var cliente = await _contexto.Clientes.FindAsync(id);

        if (cliente is null)
            return NotFound($"No se encontró el cliente con ID {id}.");

        var existeCliente = await _contexto.Clientes
            .AnyAsync(x =>
                x.Ci == entrada.Ci &&
                (x.Complemento ?? string.Empty).Trim().ToUpper() == (complementoNormalizado ?? string.Empty) &&
                x.IdCliente != id);

        if (existeCliente)
            return Conflict($"Ya existe otro cliente con el CI {entrada.Ci} {complementoNormalizado}.");

        cliente.Ci                 = entrada.Ci;
        cliente.Complemento        = complementoNormalizado;
        cliente.NombreCompleto     = entrada.NombreCompleto.Trim();
        cliente.Telefono           = entrada.Telefono?.Trim();
        cliente.Correo             = entrada.Correo?.Trim();
        cliente.FechaActualizacion = DateTime.UtcNow;

        await _contexto.SaveChangesAsync();

        var salida = new ActualizarClienteOutput
        {
            IdCliente          = cliente.IdCliente,
            Ci                 = cliente.Ci,
            Complemento        = cliente.Complemento,
            NombreCompleto     = cliente.NombreCompleto,
            Telefono           = cliente.Telefono,
            Correo             = cliente.Correo,
            EstaActivo         = cliente.EstaActivo,
            FechaRegistro      = cliente.FechaRegistro,
            FechaActualizacion = cliente.FechaActualizacion
        };

        return Ok(salida);
    }


    [HttpPatch("{id:guid}/estado")]
    public async Task<ActionResult<CambiarEstadoClienteOutput>> PatchEstadoCliente(Guid id, [FromBody] CambiarEstadoClienteInput entrada)
    {
        if (entrada.EstaActivo is null)
            return BadRequest("El estado es obligatorio.");

        var cliente = await _contexto.Clientes.FindAsync(id);

        if (cliente is null)
            return NotFound($"No se encontró el cliente con ID {id}.");

        if (cliente.EstaActivo == entrada.EstaActivo.Value)
            return Conflict($"El cliente ya se encuentra {(entrada.EstaActivo.Value ? "activo" : "inactivo")}.");

        cliente.EstaActivo         = entrada.EstaActivo.Value;
        cliente.FechaActualizacion = DateTime.UtcNow;

        await _contexto.SaveChangesAsync();

        var salida = new CambiarEstadoClienteOutput
        {
            IdCliente      = cliente.IdCliente,
            NombreCompleto = cliente.NombreCompleto,
            EstaActivo     = cliente.EstaActivo
        };

        return Ok(salida);
    }


   
    [HttpGet("BuscarClientePorCi")]
    [ActionName("BuscarPorCi")]
    public async Task<ActionResult<BuscarClienteCiOutput>> BuscarClientePorCi(
        [FromQuery][Required] int ci,
        [FromQuery] string? complemento)
    {
        var complementoNormalizado = complemento?.Trim().ToUpper();

        var cliente = await _contexto.Clientes
            .AsNoTracking()
            .Where(x =>
                x.Ci == ci &&
                (x.Complemento ?? string.Empty).Trim().ToUpper() == (complementoNormalizado ?? string.Empty))
            .Select(x => new BuscarClienteCiOutput
            {
                IdCliente      = x.IdCliente,
                Ci             = x.Ci,
                Complemento    = x.Complemento,
                NombreCompleto = x.NombreCompleto,
                Telefono       = x.Telefono,
                Correo         = x.Correo,
                EstaActivo     = x.EstaActivo
            })
            .FirstOrDefaultAsync();

        if (cliente is null)
            return NotFound($"No se encontró ningún cliente con el CI {ci} {complementoNormalizado}.");

        return Ok(cliente);
    }

    
    [HttpGet("buscar-por-nombre")]
    [ActionName("BuscarPorNombre")]
    public async Task<ActionResult<ICollection<BuscarClientePorNombreOutput>>> BuscarClientePorNombre(
        [FromQuery][Required] string nombre)
    {
        var nombreNormalizado = nombre.Trim().ToLower();

        var clientes = await _contexto.Clientes
            .AsNoTracking()
            .Where(x => x.NombreCompleto.ToLower().Contains(nombreNormalizado))
            .Select(x => new BuscarClientePorNombreOutput
            {
                IdCliente      = x.IdCliente,
                Ci             = x.Ci,
                Complemento    = x.Complemento,
                NombreCompleto = x.NombreCompleto,
                Telefono       = x.Telefono,
                EstaActivo     = x.EstaActivo
            })
            .ToListAsync();

        if (!clientes.Any())
            return NotFound($"No se encontró ningún cliente con el nombre '{nombre}'.");

        return Ok(clientes);
    }

    // GET: api/clientes/{id}/historial-compras
    [HttpGet("{id:guid}/historial-compras")]
    [ActionName("HistorialCompras")]
    public async Task<ActionResult<ICollection<HistorialComprasPorClienteOutput>>> GetHistorialCompras(Guid id)
    {
        var existeCliente = await _contexto.Clientes
            .AnyAsync(x => x.IdCliente == id);

        if (!existeCliente)
            return NotFound($"No se encontró el cliente con ID {id}.");

        var historial = await _contexto.Ventas
            .AsNoTracking()
            .Where(x => x.IdCliente == id)
            .OrderByDescending(x => x.FechaVenta)
            .Select(x => new HistorialComprasPorClienteOutput
            {
                IdVenta     = x.IdVenta,
                FechaVenta  = x.FechaVenta,
                MetodoPago  = x.MetodoPago,
                EstadoVenta = x.EstadoVenta,
                Observacion = x.Observacion,
                Total       = x.Total,
                Detalles    = x.DetallesVenta
                    .Select(d => new DetalleOutput
                    {
                        IdProducto     = d.IdProducto,
                        NombreProducto = d.Producto!.NombreProducto,
                        Marca          = d.Producto.Marca,
                        Cantidad       = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario,
                        Subtotal       = d.Subtotal
                    }).ToList()
            })
            .ToListAsync();

        if (!historial.Any())
            return NotFound($"El cliente no tiene compras registradas.");

        return Ok(historial);
    }

}

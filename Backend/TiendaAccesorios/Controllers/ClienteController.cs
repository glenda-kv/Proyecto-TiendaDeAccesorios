using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    private readonly IMapper _mapper;


    public ClienteController(AppDbContext contexto, IMapper mapper)
    {
        _contexto = contexto;
        _mapper = mapper;
    }

    [HttpGet("listar-clientes")]
    [ActionName("ListarClientes")]
    public async Task<ActionResult<ICollection<ListarClientesOutput>>> ListarClientes()
    {
        var clientes = await _contexto.Clientes
            .Where(x => x.EstaActivo)
            .OrderBy(x => x.NombreCompleto)
            .ProjectTo<ListarClientesOutput>(_mapper.ConfigurationProvider)
            .ToListAsync();

        if (!clientes.Any())
            return NotFound(new { mensaje = "No hay clientes registrados." });

        return Ok(clientes);
    }


    [HttpGet("{id:guid}/obtener-cliente")]
    [ActionName("ObtenerCliente")]
    public async Task<ActionResult<ObtenerClienteOutput>> ObtenerCliente(Guid id)
    {
        var cliente = await _contexto.Clientes
            .Where(x => x.IdCliente == id)
            .ProjectTo<ObtenerClienteOutput>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (cliente is null)
            return NotFound(new { mensaje = "Cliente no encontrado." });

        return Ok(cliente);
    }

    [HttpGet("buscar-cliente-ci")]
    [ActionName("BuscarClientePorCi")]
    public async Task<ActionResult<BuscarClienteCiOutput>> BuscarClientePorCi(
        [FromQuery] BuscarClienteCiQuery entrada)
    {
        var complementoNormalizado = entrada.Complemento?.Trim().ToUpper();

        var candidatos = await _contexto.Clientes
            .Where(x => x.Ci == entrada.Ci)
            .ToListAsync();

        var entidad = candidatos
            .FirstOrDefault(x =>
                (x.Complemento ?? string.Empty).Trim().ToUpper() ==
                (complementoNormalizado ?? string.Empty));

        if (entidad is null)
            return NotFound(new { mensaje = $"No se encontró ningún cliente con el CI {entrada.Ci} {complementoNormalizado}." });

        var salida = _mapper.Map<BuscarClienteCiOutput>(entidad);
        return Ok(salida);
    }

    [HttpGet("buscar-cliente-nombre")]
    [ActionName("BuscarClientePorNombre")]
    public async Task<ActionResult<ICollection<BuscarClientePorNombreOutput>>> BuscarClientePorNombre(
        [FromQuery][Required] string nombre)
    {
        var nombreNormalizado = nombre.Trim().ToLower();

        var clientes = await _contexto.Clientes
            .Where(x => x.NombreCompleto.ToLower().Contains(nombreNormalizado))
            .OrderBy(x => x.NombreCompleto)
            .ProjectTo<BuscarClientePorNombreOutput>(_mapper.ConfigurationProvider)
            .ToListAsync();

        if (!clientes.Any())
            return NotFound(new { mensaje = $"No se encontró ningún cliente con el nombre '{nombre}'." });

        return Ok(clientes);
    }

    [HttpGet("{id:guid}/historial-compras")]
    [ActionName("HistorialComprasCliente")]
    public async Task<ActionResult<ICollection<HistorialComprasPorClienteOutput>>> HistorialComprasCliente(Guid id)
    {
        var existeCliente = await _contexto.Clientes
            .AnyAsync(x => x.IdCliente == id);

        if (!existeCliente)
            return NotFound(new { mensaje = "Cliente no encontrado." });

        var historial = await _contexto.Ventas
            .Where(x => x.IdCliente == id)
            .OrderByDescending(x => x.FechaVenta)
            .ProjectTo<HistorialComprasPorClienteOutput>(_mapper.ConfigurationProvider)
            .ToListAsync();

        if (!historial.Any())
            return NotFound(new { mensaje = "El cliente no tiene compras registradas." });

        return Ok(historial);
    }

    [HttpPost("agregar-cliente")]
    [ActionName("AgregarCliente")]
    public async Task<ActionResult<AgregarClienteOutput>> AgregarCliente([FromBody] AgregarClienteInput entrada)
    {
        var complementoNormalizado = entrada.Complemento?.Trim().ToUpper();
        var nombreNormalizado = entrada.NombreCompleto.Trim();

        var candidatos = await _contexto.Clientes
            .Where(x => x.Ci == entrada.Ci)
            .ToListAsync();

        var existeCliente = candidatos.Any(x =>
            (x.Complemento ?? string.Empty).Trim().ToUpper() ==
            (complementoNormalizado ?? string.Empty));

        if (existeCliente)
            return Conflict(new { mensaje = $"Ya existe un cliente con el CI {entrada.Ci} {complementoNormalizado}." });

        var cliente = _mapper.Map<Cliente>(entrada);
        cliente.Complemento = complementoNormalizado;
        cliente.NombreCompleto = nombreNormalizado;

        _contexto.Clientes.Add(cliente);
        await _contexto.SaveChangesAsync();

        var salida = _mapper.Map<AgregarClienteOutput>(cliente);
        return CreatedAtAction(nameof(ObtenerCliente), new { id = cliente.IdCliente }, salida);
    }

    [HttpPut("{id:guid}/actualizar")]
    [ActionName("ActualizarCliente")]
    public async Task<ActionResult<ActualizarClienteOutput>> ActualizarCliente(Guid id, [FromBody] ActualizarClienteInput entrada)
    {
        var complementoNormalizado = entrada.Complemento?.Trim().ToUpper();
        var nombreNormalizado = entrada.NombreCompleto.Trim();

        var cliente = await _contexto.Clientes.FindAsync(id);

        if (cliente is null)
            return NotFound(new { mensaje = "Cliente no encontrado." });

        var candidatos = await _contexto.Clientes
            .Where(x => x.Ci == entrada.Ci && x.IdCliente != id)
            .ToListAsync();

        var existeCliente = candidatos.Any(x =>
            (x.Complemento ?? string.Empty).Trim().ToUpper() ==
            (complementoNormalizado ?? string.Empty));

        if (existeCliente)
            return Conflict(new { mensaje = $"Ya existe otro cliente con el CI {entrada.Ci} {complementoNormalizado}." });

        _mapper.Map(entrada, cliente);
        cliente.Complemento = complementoNormalizado;
        cliente.NombreCompleto = nombreNormalizado;
        cliente.FechaActualizacion = DateTime.UtcNow;

        await _contexto.SaveChangesAsync();

        var salida = _mapper.Map<ActualizarClienteOutput>(cliente);
        return Ok(salida);
    }

    [HttpPatch("{id:guid}/estado")]
    [ActionName("CambiarEstadoCliente")]
    public async Task<ActionResult<CambiarEstadoClienteOutput>> CambiarEstadoCliente(Guid id, [FromBody] CambiarEstadoClienteInput entrada)
    {
        var cliente = await _contexto.Clientes.FindAsync(id);

        if (cliente is null)
            return NotFound(new { mensaje = "Cliente no encontrado." });

        if (cliente.EstaActivo == entrada.EstaActivo!.Value)
            return Conflict(new { mensaje = $"El cliente ya se encuentra {(entrada.EstaActivo.Value ? "activo" : "inactivo")}." });

        cliente.EstaActivo = entrada.EstaActivo.Value;
        cliente.FechaActualizacion = DateTime.UtcNow;

        await _contexto.SaveChangesAsync();

        var salida = _mapper.Map<CambiarEstadoClienteOutput>(cliente);
        return Ok(salida);
    }

}
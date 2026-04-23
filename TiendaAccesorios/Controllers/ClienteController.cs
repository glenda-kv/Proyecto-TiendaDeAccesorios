using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaAccesorios.Data;
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
    public async Task<ActionResult<ICollection<Cliente>>> GetClientes()
    {
        var clientes = await _contexto.Clientes.ToListAsync();
        return Ok(clientes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cliente>> GetCliente(Guid id)
    {
        var cliente = await _contexto.Clientes.FindAsync(id);

        if (cliente == null)
            return NotFound("Cliente no encontrado");

        return Ok(cliente);
    }

    [HttpPost]
    public async Task<ActionResult<Cliente>> CreateCliente([FromBody] Cliente cliente)
    {
        cliente.IdCliente = Guid.NewGuid();
        cliente.FechaRegistro = DateTime.UtcNow;
        cliente.Estado = true;

        _contexto.Clientes.Add(cliente);
        await _contexto.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCliente), new { id = cliente.IdCliente }, cliente);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCliente(Guid id, [FromBody] Cliente cliente)
    {
        var existing = await _contexto.Clientes.FindAsync(id);

        if (existing == null)
            return NotFound("Cliente no encontrado");

        existing.Ci = cliente.Ci;
        existing.Extension = cliente.Extension;
        existing.NombreCompleto = cliente.NombreCompleto;
        existing.Telefono = cliente.Telefono;
        existing.Estado = cliente.Estado;

        await _contexto.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCliente(Guid id)
    {
        var cliente = await _contexto.Clientes.FindAsync(id);

        if (cliente == null)
            return NotFound("Cliente no encontrado");

        _contexto.Clientes.Remove(cliente);
        await _contexto.SaveChangesAsync();

        return NoContent();
    }

}

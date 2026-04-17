using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaAccesorios.Data;
using TiendaAccesorios.Entidades;

namespace TiendaAccesorios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly AppDbContext _contexto;

        public ProveedorController(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Proveedor>>> GetProveedores()
        {
            var proveedores = await _contexto.Proveedores.ToListAsync();
            return Ok(proveedores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor>> GetProveedor(Guid id)
        {
            var proveedor = await _contexto.Proveedores.FindAsync(id);

            if (proveedor == null)
                return NotFound();

            return Ok(proveedor);
        }

        [HttpPost]
        public async Task<ActionResult<Proveedor>> CreateProveedor([FromBody] Proveedor proveedor)
        {
            _contexto.Proveedores.Add(proveedor);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProveedor), new { id = proveedor.IdProveedor }, proveedor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProveedor(Guid id, [FromBody] Proveedor proveedor)
        {
            if (id != proveedor.IdProveedor)
                return BadRequest();

            var existing = await _contexto.Proveedores.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.NombreCompleto = proveedor.NombreCompleto;
            existing.Telefono = proveedor.Telefono;
            existing.Correo = proveedor.Correo;
            existing.Direccion = proveedor.Direccion;
            existing.Estado = proveedor.Estado;

            await _contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(Guid id)
        {
            var proveedor = await _contexto.Proveedores.FindAsync(id);
            if (proveedor == null)
                return NotFound();

            _contexto.Proveedores.Remove(proveedor);
            await _contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}

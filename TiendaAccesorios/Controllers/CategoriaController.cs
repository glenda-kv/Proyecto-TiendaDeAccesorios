using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaAccesorios.Data;
using TiendaAccesorios.Entidades;

namespace TiendaAccesorios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
         private readonly AppDbContext _contexto;

        public CategoriaController(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Categoria>>> GetCategorias()
        {
            var categorias = await _contexto.Categorias.ToListAsync();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(Guid id)
        {
            var categoria = await _contexto.Categorias.FindAsync(id);

            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> CreateCategoria([FromBody] Categoria categoria)
        {
            _contexto.Categorias.Add(categoria);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategoria), new { id = categoria.IdCategoria }, categoria);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoria(Guid id, [FromBody] Categoria categoria)
        {
            if (id != categoria.IdCategoria)
                return BadRequest();

            var existing = await _contexto.Categorias.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.NombreCategoria = categoria.NombreCategoria;
            existing.Descripcion = categoria.Descripcion;
            existing.Estado = categoria.Estado;

            await _contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(Guid id)
        {
            var categoria = await _contexto.Categorias.FindAsync(id);
            if (categoria == null)
                return NotFound();

            _contexto.Categorias.Remove(categoria);
            await _contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}

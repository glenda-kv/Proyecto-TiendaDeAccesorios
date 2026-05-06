using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaAccesorios.Data;
using TiendaAccesorios.DTO.Categoria.ActualizarCategoria;
using TiendaAccesorios.DTO.Categoria.AgregarCategoria;
using TiendaAccesorios.DTO.Categoria.CambiarEstadoCategoria;
using TiendaAccesorios.DTO.Categoria.ListarCategorias;
using TiendaAccesorios.DTO.Categoria.ObtenerCategoria;
using TiendaAccesorios.Entidades;

namespace TiendaAccesorios.Controllers
{
    public class CategoriaController : BaseApiController
    {
        private readonly AppDbContext _contexto;

        public CategoriaController(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet("ListarTodos")]
        public async Task<ActionResult<ICollection<ListarCategoriasOutput>>> GetCategorias()
        {
            var categorias = await _contexto.Categorias
                .AsNoTracking()
                .Where(x => x.EstaActivo)
                .Select(x => new ListarCategoriasOutput
                {
                    IdCategoria     = x.IdCategoria,
                    NombreCategoria = x.NombreCategoria,
                    Descripcion     = x.Descripcion
                })
                .ToListAsync();

            return Ok(categorias);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ObtenerCategoriaOutput>> GetCategoria(Guid id)
        {
            var categoria = await _contexto.Categorias
                .AsNoTracking()
                .Where(x => x.IdCategoria == id)
                .Select(x => new ObtenerCategoriaOutput
                {
                    IdCategoria     = x.IdCategoria,
                    NombreCategoria = x.NombreCategoria,
                    Descripcion     = x.Descripcion,
                    EstaActivo      = x.EstaActivo
                })
                .FirstOrDefaultAsync();

            if (categoria is null)
                return NotFound($"No se encontró la categoría con ID {id}");

            return Ok(categoria);
        }
        
        // POST: api/categorias
        [HttpPost]
        public async Task<ActionResult<AgregarCategoriaOutput>> CreateCategoria([FromBody] AgregarCategoriaInput categoria)
        {
            var existeNombre = await _contexto.Categorias
                .AnyAsync(x => x.NombreCategoria == categoria.NombreCategoria);

            if (existeNombre)
                return Conflict($"Ya existe una categoría con el nombre '{categoria.NombreCategoria}'.");

            var entrada = new Categoria
            {
                NombreCategoria = categoria.NombreCategoria,
                Descripcion     = categoria.Descripcion,
                EstaActivo      = true
            };

            _contexto.Categorias.Add(entrada);
            await _contexto.SaveChangesAsync();

            var salida = new AgregarCategoriaOutput
            {
                IdCategoria     = entrada.IdCategoria,
                NombreCategoria = entrada.NombreCategoria,
                Descripcion     = entrada.Descripcion
            };

            return CreatedAtAction(nameof(GetCategoria), new { id = salida.IdCategoria }, salida);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ActualizarCategoriaOutput>> UpdateCategoria(Guid id, [FromBody] ActualizarCategoriaInput entrada)
        {
            var categoria = await _contexto.Categorias.FindAsync(id);

            if (categoria is null)
                return NotFound($"No se encontró la categoría con ID {id}.");

            var existeNombre = await _contexto.Categorias
                .AnyAsync(x => x.NombreCategoria == entrada.NombreCategoria && x.IdCategoria != id);

            if (existeNombre)
                return Conflict($"Ya existe una categoría con el nombre '{entrada.NombreCategoria}'.");

            categoria.NombreCategoria = entrada.NombreCategoria;
            categoria.Descripcion     = entrada.Descripcion;

            await _contexto.SaveChangesAsync();

            var salida = new ActualizarCategoriaOutput
            {
                IdCategoria     = categoria.IdCategoria,
                NombreCategoria = categoria.NombreCategoria,
                Descripcion     = categoria.Descripcion,
                EstaActivo      = categoria.EstaActivo
            };

            return Ok(salida);
        }

        
        [HttpPatch("{id:guid}/estado")]
        public async Task<ActionResult<CambiarEstadoCategoriaOutput>> PatchEstadoCategoria(Guid id, [FromBody] CambiarEstadoCategoriaInput entrada)
        {
            if (entrada.EstaActivo is null)
                return BadRequest("El estado es obligatorio.");

            var categoria = await _contexto.Categorias.FindAsync(id);

            if (categoria is null)
                return NotFound($"No se encontró la categoría con ID {id}.");

            if (categoria.EstaActivo == entrada.EstaActivo.Value)
                return Conflict($"La categoría ya se encuentra {(entrada.EstaActivo.Value ? "activa" : "inactiva")}.");

            categoria.EstaActivo = entrada.EstaActivo.Value;

            await _contexto.SaveChangesAsync();

            var salida = new CambiarEstadoCategoriaOutput
            {
                IdCategoria     = categoria.IdCategoria,
                NombreCategoria = categoria.NombreCategoria,
                EstaActivo      = categoria.EstaActivo
            };

            return Ok(salida);
        }
        
    }
}

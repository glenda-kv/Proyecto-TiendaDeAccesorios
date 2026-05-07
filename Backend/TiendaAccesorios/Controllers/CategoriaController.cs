using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IMapper _mapper;


        public CategoriaController(AppDbContext contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        [HttpGet("listar-categorias")] 
        [ActionName("ListarCategorias")]
        public async Task<ActionResult<ICollection<ListarCategoriasOutput>>> ListarCategorias()
        {
            var categorias = await _contexto.Categorias
                .Where(x => x.EstaActivo)
                .OrderBy(x => x.NombreCategoria)
                .ProjectTo<ListarCategoriasOutput>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (!categorias.Any())
                return NotFound(new { mensaje = "No hay categorías registradas." });

            return Ok(categorias);
        }

        [HttpGet("{id:guid}/obtener-categoria")] 
        [ActionName("ObtenerCategoria")]
        public async Task<ActionResult<ObtenerCategoriaOutput>> ObtenerCategoria(Guid id)
        {
            var categoria = await _contexto.Categorias
                .Where(x => x.IdCategoria == id)
                .ProjectTo<ObtenerCategoriaOutput>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (categoria is null)
                return NotFound(new { mensaje = "Categoría no encontrada." });

            return Ok(categoria);
        }
        
        
        [HttpPost("agregar-categoria")] 
        [ActionName("AgregarCategoria")]
        public async Task<ActionResult<AgregarCategoriaOutput>> AgregarCategoria([FromBody] AgregarCategoriaInput entrada)
        {
            var existeNombre = await _contexto.Categorias
                .AnyAsync(x => x.NombreCategoria == entrada.NombreCategoria);

            if (existeNombre)
                return Conflict(new { mensaje = $"Ya existe una categoría con el nombre '{entrada.NombreCategoria}'." });

            var categoria = _mapper.Map<Categoria>(entrada);

            _contexto.Categorias.Add(categoria);
            await _contexto.SaveChangesAsync();

            var salida = _mapper.Map<AgregarCategoriaOutput>(categoria);
            return CreatedAtAction(nameof(ObtenerCategoria), new { id = categoria.IdCategoria }, salida);
        }

        [HttpPut("{id:guid}/actualizar")]
        [ActionName("ActualizarCategoria")]
        public async Task<ActionResult<ActualizarCategoriaOutput>> ActualizarCategoria(Guid id, [FromBody] ActualizarCategoriaInput entrada)
        {
            var categoria = await _contexto.Categorias.FindAsync(id);

            if (categoria is null)
                return NotFound(new { mensaje = "Categoría no encontrada." });

            var existeNombre = await _contexto.Categorias
                .AnyAsync(x => x.NombreCategoria == entrada.NombreCategoria && x.IdCategoria != id);

            if (existeNombre)
                return Conflict(new { mensaje = $"Ya existe una categoría con el nombre '{entrada.NombreCategoria}'." });

            _mapper.Map(entrada, categoria);
            await _contexto.SaveChangesAsync();

            var salida = _mapper.Map<ActualizarCategoriaOutput>(categoria);
            return Ok(salida);
        }

        
        [HttpPatch("{id:guid}/estado")]
        [ActionName("CambiarEstadoCategoria")]
        public async Task<ActionResult<CambiarEstadoCategoriaOutput>> CambiarEstadoCategoria(Guid id, [FromBody] CambiarEstadoCategoriaInput entrada)
        {
            var categoria = await _contexto.Categorias.FindAsync(id);

            if (categoria is null)
                return NotFound(new { mensaje = "Categoría no encontrada." });

            if (categoria.EstaActivo == entrada.EstaActivo!.Value)
                return Conflict(new { mensaje = $"La categoría ya se encuentra {(entrada.EstaActivo.Value ? "activa" : "inactiva")}." });

            categoria.EstaActivo = entrada.EstaActivo.Value;
            await _contexto.SaveChangesAsync();

            var salida = _mapper.Map<CambiarEstadoCategoriaOutput>(categoria);
            return Ok(salida);
        }
    }
}

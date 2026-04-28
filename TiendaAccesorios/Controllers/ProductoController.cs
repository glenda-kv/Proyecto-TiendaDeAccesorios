using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaAccesorios.Data;
using TiendaAccesorios.DTO.Producto.AgregarProducto;
using TiendaAccesorios.Entidades;

namespace TiendaAccesorios.Controllers
{
    public class ProductoController : BaseApiController
    {
        private readonly AppDbContext _contexto;

        public ProductoController(AppDbContext contexto)
        {
            _contexto = contexto;
        }
        
    
        // GET: api/productos
        [HttpGet]
        public async Task<ActionResult<ICollection<Producto>>> GetProductos()
        {
            var productos = await _contexto.Productos
                .AsNoTracking()
                .Where(x => x.EstaActivo)
                .ToListAsync();

            return Ok(productos);
        }

        // GET: api/productos/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Producto>> GetProducto(Guid id)
        {
            var producto = await _contexto.Productos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdProducto == id);

            if (producto is null)
                return NotFound($"No se encontró el producto con ID {id}.");

            return Ok(producto);
        }

        // POST: api/productos
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            producto.IdProducto   = Guid.NewGuid();
            producto.EstaActivo   = true;
            producto.FechaRegistro = DateTime.UtcNow;
            producto.FechaActualizacion = null;
            producto.FechaUltimoIngresoStock = null;

            _contexto.Productos.Add(producto);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducto), new { id = producto.IdProducto }, producto);
        }

        // PUT: api/productos/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Producto>> PutProducto(Guid id, Producto input)
        {
            var producto = await _contexto.Productos.FindAsync(id);

            if (producto is null)
                return NotFound($"No se encontró el producto con ID {id}.");

            producto.NombreProducto    = input.NombreProducto;
            producto.Descripcion       = input.Descripcion;
            producto.Marca             = input.Marca;
            producto.Color             = input.Color;
            producto.Precio            = input.Precio;
            producto.IdCategoria       = input.IdCategoria;
            producto.FechaActualizacion = DateTime.UtcNow;

            await _contexto.SaveChangesAsync();

            return Ok(producto);
        }


        
    }
}

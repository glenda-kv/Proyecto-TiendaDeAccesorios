using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaAccesorios.Data;
using TiendaAccesorios.DTO.Producto.AgregarProducto;
using TiendaAccesorios.Entidades;

namespace TiendaAccesorios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly AppDbContext _contexto;

        public ProductoController(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Producto>>> GetProductos()
        {
            var productos = await _contexto.Productos.ToListAsync();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(Guid id)
        {
            var producto = await _contexto.Productos.FindAsync(id);

            if (producto == null)
                return NotFound();

            return Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult<AgregarProductoOutput>> CreateProducto([FromBody] AgregarProductoInput producto)
        {
            var entrada = new Producto
            {
                NombreProducto = producto.NombreProducto,
                Descripcion = producto.Descripcion,
                Marca = producto.Marca,
                Color = producto.Color,
                Precio = producto.Precio,
                Stock = producto.Stock,
                IdCategoria = producto.IdCategoria,
                IdProveedor = producto.IdProveedor
            };

            entrada.IdProducto = Guid.NewGuid();
            entrada.FechaRegistro = DateTime.Now;
            entrada.Estado = true;

            _contexto.Productos.Add(entrada);
            await _contexto.SaveChangesAsync();

            var categoria = await _contexto.Categorias.FindAsync(entrada.IdCategoria);
            var proveedor = await _contexto.Proveedores.FindAsync(entrada.IdProveedor);

            var salida = new AgregarProductoOutput
            {
                IdProducto = entrada.IdProducto,
                NombreProducto = entrada.NombreProducto,
                Descripcion = entrada.Descripcion,
                Marca = entrada.Marca,
                Color = entrada.Color,
                Precio = entrada.Precio,
                Stock = entrada.Stock,
                NombreCategoria = categoria?.NombreCategoria,
                NombreProveedor = proveedor?.NombreCompleto
            };

            return CreatedAtAction(nameof(GetProducto), new { id = salida.IdProducto }, salida);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(Guid id, [FromBody] Producto producto)
        {
            if (id != producto.IdProducto)
                return BadRequest();

            var existing = await _contexto.Productos.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.NombreProducto = producto.NombreProducto;
            existing.Descripcion = producto.Descripcion;
            existing.Marca = producto.Marca;
            existing.Color = producto.Color;
            existing.Precio = producto.Precio;
            existing.Stock = producto.Stock;
            existing.Estado = producto.Estado;

            await _contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(Guid id)
        {
            var producto = await _contexto.Productos.FindAsync(id);
            if (producto == null)
                return NotFound();

            _contexto.Productos.Remove(producto);
            await _contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}

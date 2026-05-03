using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaAccesorios.Data;
using TiendaAccesorios.DTO.Producto;
using TiendaAccesorios.DTO.Producto.ActualizarPrecioProducto;
using TiendaAccesorios.DTO.Producto.ActualizarProducto;
using TiendaAccesorios.DTO.Producto.AgregarProducto;
using TiendaAccesorios.DTO.Producto.CambiarEstadoProducto;
using TiendaAccesorios.DTO.Producto.IngresoStockProducto;
using TiendaAccesorios.DTO.Producto.ListarProductos;
using TiendaAccesorios.DTO.Producto.ObtenerProducto;
using TiendaAccesorios.Entidades;

namespace TiendaAccesorios.Controllers
{
    public class ProductoController : BaseApiController
    {
        private readonly AppDbContext _contexto;
        private readonly IMapper _mapper;

        public ProductoController(AppDbContext contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ListarProductosOutput>>> GetProductos()
        {
            var productos = await _contexto.Productos
                .Where(p => p.EstaActivo)
                .OrderBy(p => p.NombreProducto)
                .ProjectTo<ListarProductosOutput>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(productos);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ObtenerProductoOutput>> GetProducto(Guid id)
        {
            var producto = await _contexto.Productos
                .Where(p => p.IdProducto == id)
                .ProjectTo<ObtenerProductoOutput>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (producto is null)
                return NotFound(new { mensaje = "Producto no encontrado." });

            return Ok(producto);
        }
        
        [HttpPost]
        public async Task<ActionResult<AgregarProductoOutput>> CreateProducto(AgregarProductoInput entrada)
        {
            var categoria = await _contexto.Categorias
                .FirstOrDefaultAsync(c => c.NombreCategoria.ToLower() == entrada.Categoria.ToLower());

            if (categoria is null)
                return NotFound(new { mensaje = "La categoría ingresada no existe." });

            var producto = _mapper.Map<Producto>(entrada);
            producto.IdCategoria = categoria.IdCategoria;
            producto.Categoria = categoria;

            _contexto.Productos.Add(producto);
            await _contexto.SaveChangesAsync();

            var salida = _mapper.Map<AgregarProductoOutput>(producto);
            return CreatedAtAction(nameof(GetProducto), new { id = producto.IdProducto }, salida);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ActualizarProductoOutput>> UpdateProducto(Guid id, ActualizarProductoInput entrada)
        {
            var producto = await _contexto.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.IdProducto == id);

            if (producto is null)
                return NotFound(new { mensaje = "Producto no encontrado." });

            var categoria = await _contexto.Categorias
                .FirstOrDefaultAsync(c => c.NombreCategoria.ToLower() == entrada.Categoria.ToLower());

            if (categoria is null)
                return NotFound(new { mensaje = "La categoría ingresada no existe." });

            _mapper.Map(entrada, producto);
            producto.IdCategoria = categoria.IdCategoria;
            producto.Categoria = categoria;

            await _contexto.SaveChangesAsync();

            var salida = _mapper.Map<ActualizarProductoOutput>(producto);
            return Ok(salida);
        }

        [HttpPatch("{id:guid}/precio")]
        public async Task<ActionResult<ActualizarPrecioProductoOutput>> ActualizarPrecio(Guid id, ActualizarPrecioProductoInput entrada)
        {
            var producto = await _contexto.Productos
                .FirstOrDefaultAsync(p => p.IdProducto == id);

            if (producto is null)
                return NotFound(new { mensaje = "Producto no encontrado." });

            producto.Precio = entrada.Precio;
            producto.FechaActualizacion = DateTime.UtcNow;

            await _contexto.SaveChangesAsync();

            var salida = _mapper.Map<ActualizarPrecioProductoOutput>(producto);
            return Ok(salida);
        }

        [HttpPatch("{id:guid}/estado")]
        public async Task<ActionResult<CambiarEstadoProductoOutput>> CambiarEstado(Guid id, CambiarEstadoProductoInput entrada)
        {
            var producto = await _contexto.Productos
                .FirstOrDefaultAsync(p => p.IdProducto == id);

            if (producto is null)
                return NotFound(new { mensaje = "Producto no encontrado." });

            producto.EstaActivo = entrada.EstaActivo;
            producto.FechaActualizacion = DateTime.UtcNow;

            await _contexto.SaveChangesAsync();

            var salida = _mapper.Map<CambiarEstadoProductoOutput>(producto);
            return Ok(salida);
        }

        [HttpPatch("{id:guid}/stock")]
        public async Task<ActionResult<IngresoStockProductoOutput>> AgregarStock(Guid id, IngresoStockProductoInput entrada)
        {
            var producto = await _contexto.Productos
                .FirstOrDefaultAsync(p => p.IdProducto == id);

            if (producto is null)
                return NotFound(new { mensaje = "Producto no encontrado." });

            producto.Stock += entrada.Unidades;
            producto.FechaUltimoIngresoStock = DateTime.UtcNow;

            await _contexto.SaveChangesAsync();

            var salida = _mapper.Map<IngresoStockProductoOutput>(producto);
            return Ok(salida);
        }



    }
}
